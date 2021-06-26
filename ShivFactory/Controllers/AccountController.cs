using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShivFactory.Models;
using ShivFactory.Business;
using ShivFactory.Business.Repository;
using DataLibrary.DL;
using System;
using ShivFactory.Business.Model;
using ShivFactory.Business.Models.Other;
using System.Security.Claims;
using ShivFactory.Business.Repository.SMS;
using ShivFactory.Business.Model.Common;
using System.Collections.Generic;
using System.Web.Security;

namespace ShivFactory.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Services
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region CustomerLogIn
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && (returnUrl.Equals("/Account/VendorLogin") || returnUrl.ToUpper().Contains("/VENDOR")))
            {
                return RedirectToAction("VendorLogin","Account", returnUrl);
                //return View("VendorLogin", returnUrl);
            }
            if (!string.IsNullOrEmpty(returnUrl) && (returnUrl.Equals("/Account/AdminLogin") || returnUrl.ToUpper().Contains("/ADMIN")))
            {
                return RedirectToAction("VendorLogin","Account", returnUrl);
                //return View("VendorLogin", returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LogInModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
            model.Role = UserRoles.Customer;
            var result = await LogInApI(model);
            if (result.ResultFlag == true)
            {
                return RedirectToLocal(result.Data, returnUrl);
            }
            else
            {
                if (result.Message.ToLower() == "invalid username")
                {
                    ModelState.AddModelError("PhoneNumber", result.Message);
                }
                else if (result.Message.ToLower() == "invalid password.")
                {
                    ModelState.AddModelError("Password", result.Message);
                }
                else
                {
                    ModelState.AddModelError("RememberMe", result.Message);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LogInCustomer(LogInModel model, string returnUrl)
        {
            try
            {
                List<ErrorModel> Errors = new List<ErrorModel>();
                bool resultFlag = true;
                if (!ModelState.IsValid)
                {
                    resultFlag = false;
                }
                else
                {
                    model.Role = UserRoles.Customer;
                    var result = await LogInApI(model);
                    if (result.ResultFlag == true)
                    {
                        //return RedirectToLocal(result.Data, returnUrl);
                        RepoCookie co = new RepoCookie();
                        co.AddCookiesValues(result.Data);
                        if (!Url.IsLocalUrl(returnUrl))
                        {
                            returnUrl = "/Home/Index";
                        }
                    }
                    else
                    {
                        if (result.Message.ToLower() == "invalid username")
                        {
                            ModelState.AddModelError("PhoneNumber", result.Message);
                        }
                        else if (result.Message.ToLower() == "invalid password.")
                        {
                            ModelState.AddModelError("Password", result.Message);
                        }
                        else
                        {
                            ModelState.AddModelError("RememberMe", result.Message);
                        }
                        resultFlag = false;
                    }
                }
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    Errors.Add(new ErrorModel()
                    {
                        Key = modelStateKey,
                        Values = ViewData.ModelState[modelStateKey].Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }
                return Json(new ResultModel { ResultFlag = resultFlag, Data = Errors, Message = returnUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel { ResultFlag = false, Data = null, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region VendorLogIn
         [AllowAnonymous]
        public ActionResult VendorLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> VendorLogin(LogInModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
            var result = await LogInApI(model);
            if (result.ResultFlag == true)
            {
                return RedirectToLocal(result.Data, returnUrl);
            }
            else
            {
                if (result.Message.ToLower() == "invalid username")
                {
                    ModelState.AddModelError("PhoneNumber", result.Message);
                }
                else if (result.Message.ToLower() == "invalid password.")
                {
                    ModelState.AddModelError("Password", result.Message);
                }
                return View(model);
            }
            return View(model);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(null, model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        #endregion

        #region User Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(CustomerRegister model, string returnUrl)
        {
            List<ErrorModel> Errors = new List<ErrorModel>();
            bool resultFlag = true;
            if (!ModelState.IsValid)
            {
                resultFlag = false;
            }
            else
            {
                var res = await CustomerRegister(model);
                if (res.ResultFlag == true)
                {
                    // add cookie data after login 
                    LogInModel login = new LogInModel();
                    login.Role = UserRoles.Customer;
                    login.PhoneNumber = model.PhoneNumber;
                    login.Password = model.Password;
                    var result = await LogInApI(login);
                    if (result.ResultFlag == true)
                    {
                        // return RedirectToLocal(result.Data, "/Home/Index");
                    }

                }
                else
                {
                    resultFlag = res.ResultFlag;
                    ModelState.AddModelError("", res.Message);
                    // return View(model);
                }
            }
            foreach (var modelStateKey in ViewData.ModelState.Keys)
            {
                Errors.Add(new ErrorModel()
                {
                    Key = modelStateKey,
                    Values = ViewData.ModelState[modelStateKey].Errors.Select(e => e.ErrorMessage).ToList()
                });
            }
            return Json(new ResultModel { ResultFlag = resultFlag, Data = Errors, Message = "/Home/Index" }, JsonRequestBehavior.AllowGet);


            // If we got this far, something failed, redisplay form
            // return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        #endregion

        #region ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.PhoneNumber);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            ResetPasswordViewModel resetmodel = (ResetPasswordViewModel)TempData["reset"];
            return View(resetmodel);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.PhoneNumber);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {

                RepoUser repoUser = new RepoUser();
                bool isPasswordUpdate = repoUser.UpdateUserPassword(user.Id, model.Password);
                if (isPasswordUpdate)
                {

                    LogInModel loginmodel = new LogInModel
                    {

                        PhoneNumber = model.PhoneNumber,
                        Password = model.Password
                    };
                    var loginresult = await LogInApI(loginmodel);
                    if (loginresult.ResultFlag == true)
                    {
                        return RedirectToLocal(loginresult.Data, "");
                    }


                }
            }
            ModelState.AddModelError("Password", result.Errors.FirstOrDefault());
            //AddErrors(result);
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion

        #region ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(null, returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(null, returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        #endregion

        #region LogOff
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            RepoCookie co = new RepoCookie();
            co.ClearCookiesValues();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        #endregion

        #region Create api 

        #region User Register Api

        #region Admin Register Api
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResultModel> AdminRegister(CustomerRegister model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = new ApplicationUser { UserName = model.PhoneNumber, Email = model.EmailId, EmailConfirmed = true };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        UserManager.AddToRole(user.Id, UserRoles.Admin);

                        var userDetails = new UserDetail()
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Gender = model.Gender,
                            Address = model.Address,
                            Email = model.EmailId,
                            Password = model.Password,
                            Mobile = model.PhoneNumber,
                            AddDate = DateTime.Now,
                            IsActive = true,
                            IsDelete = false,
                            UserId = user.Id
                        };

                        RepoUser ru = new RepoUser();
                        var isSaved = ru.AddOrUpdateUserDetails(userDetails);

                        return new ResultModel
                        {
                            ResultFlag = isSaved,
                            Data = "",
                            Message = "User successfully Register !!"
                        };
                    }
                    else
                    {
                        return new ResultModel
                        {
                            ResultFlag = false,
                            Data = "",
                            Message = result.Errors.FirstOrDefault()
                        };
                    }
                }
                else
                {
                    return new ResultModel
                    {
                        ResultFlag = false,
                        Data = "",
                        Message = "Please enter all required fields."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    ResultFlag = false,
                    Message = ex.Message
                };
            }
        }
        #endregion

        #region Vendor Register Api
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResultModel> VendorRegister(VendorRegister model)
        {
            try
            {
                if (ModelState.IsValidField("FirstName") && ModelState.IsValidField("Email") && ModelState.IsValidField("PhoneNumber"))
                {

                    var user = new ApplicationUser { UserName = model.PhoneNumber, Email = model.EmailId, EmailConfirmed = true };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        UserManager.AddToRole(user.Id, UserRoles.Vendor);

                        var userDetails = new UserDetail()
                        {
                            FirstName = model.FirstName,
                            Email = model.EmailId,
                            Password = model.Password,
                            Mobile = model.PhoneNumber,
                            AddDate = DateTime.Now,
                            IsActive = true,
                            IsDelete = false,
                            UserId = user.Id
                        };

                        RepoUser ru = new RepoUser();
                        var isSaved = ru.AddOrUpdateUserDetails(userDetails);
                        Vendor vendor = new Vendor();
                        vendor.UserId = userDetails.UserId;
                        RepoVendor repoVendor = new RepoVendor();
                        repoVendor.AddOrUpdateVendor(vendor);
                        int vendorID = repoVendor.GetVendorIdByUserId(user.Id);
                        DataLibrary.DL.VendorBankDetail vendorBankdtls = new DataLibrary.DL.VendorBankDetail();
                        vendorBankdtls.UserID = vendorID;
                        repoVendor.AddVendorBankDetails(vendorBankdtls);


                        return new ResultModel
                        {
                            ResultFlag = isSaved,
                            Data = user.Id,
                            Message = "User successfully Register !!"
                        };
                    }
                    else
                    {
                        return new ResultModel
                        {
                            ResultFlag = false,
                            Data = "",
                            Message = result.Errors.FirstOrDefault()
                        };
                    }
                }
                else
                {
                    return new ResultModel
                    {
                        ResultFlag = false,
                        Data = "",
                        Message = "Please enter all required fields."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    ResultFlag = false,
                    Message = ex.Message
                };
            }
        }
        #endregion

        #region Customer Register Api
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultModel> CustomerRegister(CustomerRegister model)
        {
            try
            {

                var user = new ApplicationUser { UserName = model.PhoneNumber, Email = model.EmailId, EmailConfirmed = true };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    UserManager.AddToRole(user.Id, UserRoles.Customer);

                    var userDetails = new UserDetail()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Gender = model.Gender,
                        Address = model.Address,
                        Email = model.EmailId,
                        Password = model.Password,
                        Mobile = model.PhoneNumber,
                        AddDate = DateTime.Now,
                        IsActive = true,
                        IsDelete = false,
                        UserId = user.Id
                    };

                    RepoUser ru = new RepoUser();
                    var isSaved = ru.AddOrUpdateUserDetails(userDetails);

                    return new ResultModel
                    {
                        ResultFlag = isSaved,
                        Data = "",
                        Message = "User successfully Register !!"
                    };

                }
                else
                {
                    return new ResultModel
                    {
                        ResultFlag = false,
                        Data = "",
                        Message = result.Errors.FirstOrDefault()
                    };
                }

            }

            catch (Exception ex)
            {
                return new ResultModel
                {
                    ResultFlag = false,
                    Message = ex.Message
                };
            }
        }
        #endregion

        #endregion

        #region LogInApi
        [AllowAnonymous]
        public async Task<ResultModel> LogInApI(LogInModel model)
        {
            try
            {
                string message = "";

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var user = UserManager.FindByName(model.PhoneNumber);
                if (user == null)
                {
                    message = "Invalid UserName";
                }
                else
                {
                    var result = await SignInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, model.RememberMe, shouldLockout: false);
                    switch (result)
                    {
                        case SignInStatus.Success:

                            message = "Successfully LogIn.";
                            break;

                        case SignInStatus.LockedOut:
                            message = "Account is Lockout, please contact to administration.";
                            break;
                        case SignInStatus.RequiresVerification:
                            message = "Account not approved, please contact to administration.";
                            break;
                        case SignInStatus.Failure:
                            message = "Invalid password.";
                            break;
                        default:
                            message = "Invalid login attempt.";
                            break;
                    }
                    if (result == SignInStatus.Success)
                    {
                        var role = UserManager.GetRoles(user.Id).FirstOrDefault();
                        RepoUser ru = new RepoUser();
                        if (!user.EmailConfirmed)
                        {
                            message = "Email not confirmed.";
                        }
                        else if (ru.UserIsDelete(user.Id))
                        {
                            message = "Your accounts blocked, please contact administrator.";
                        }
                        else if (!string.IsNullOrEmpty(model.Role) && model.Role != role)
                        {
                            message = $"You are not register as {model.Role}.";
                        }
                        else
                        {
                            RepoCookie co = new RepoCookie();
                            int tempOrderId = co.GetIntCookiesValue(CookieName.TempOrderId);
                            var userDetails = ru.GetUserDetailsBYUserId(user.Id);
                            var token = UserManager.GenerateUserToken("", user.Id);

                            var res = new LogInResponse
                            {
                                UserId = user.Id,
                                UserName = user.UserName,
                                Role = role,
                                FirstName = userDetails.FirstName,
                                LastName = userDetails.LastName,
                                Gender = userDetails.Gender,
                                EmailId = userDetails.Email,
                                Address = userDetails.Address,
                                Mobile = userDetails.Mobile,
                                TempOrderId = userDetails.TempOrderId != null && userDetails.TempOrderId > 0 ? userDetails.TempOrderId.Value : tempOrderId
                            };

                            // add authontication
                            ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                            identity.AddClaim(new Claim(ClaimTypes.Role, role));
                            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                            AuthenticationManager.SignIn(identity);

                            co.AddCookiesValues(res);
                            return new ResultModel
                            {
                                ResultFlag = true,
                                Data = res,
                                Message = "User Successfully Login"
                            };

                        }
                    }
                }
                return new ResultModel
                {
                    ResultFlag = false,
                    Message = message
                };
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    ResultFlag = false,
                    Message = ex.Message
                };
            }
        }
        #endregion

        #region ChangePassword Api
        public ActionResult UpdateCurrentUserPassword(SetPasswordViewModel model)
        {
            var isChange = false; string message = "";
            try
            {
                Utility util = new Utility();
                string userid = util.GetCurrentUserId();
                var token = UserManager.GeneratePasswordResetToken(userid);
                var result = UserManager.ResetPasswordAsync(userid, token, model.NewPassword);
                if (result.Result.Succeeded)
                {
                    RepoUser repoUser = new RepoUser();
                    isChange = repoUser.UpdateUserPassword(userid, model.NewPassword);
                    message = isChange ? "Password Update successfully!!" : "Failled to Update Password";
                }
                else
                {
                    message = result.Result.Errors.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
            }

            return Json(new ResultModel
            {
                ResultFlag = isChange,
                Data = null,
                Message = message,
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(LogInResponse res, string returnUrl)
        {
            RepoCookie co = new RepoCookie();
            co.AddCookiesValues(res);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else if (res.Role == UserRoles.Admin)
            {
                return RedirectToAction("Index", "Admin", new { Area = "Admin" });
            }
            else if (res.Role == UserRoles.Vendor)
            {
                return RedirectToAction("Index", "Vendor", new { Area = "Vendor" });
            }
            else if (res.Role == UserRoles.Customer)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion


        #region Register Vendor
        [AllowAnonymous]
        public ActionResult MobileVerify()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult MobileVerify(VendorRegister model)
        {
            if (!ModelState.IsValidField("PhoneNumber"))
            {
                return View(model);
            }
            else
            {
                var user = UserManager.FindByName(model.PhoneNumber);
                if (user == null)
                {
                    RepoCommon repoCommon = new RepoCommon();
                    string otp = repoCommon.sendOtpSMS(model.PhoneNumber);
                    model.isOTPSend = true;
                    TempData["SuccessMessage"] = "We have sent an One Time Password (OTP) in a SMS to this mobile number.";
                    TempData["showbtn"] = true;
                    return View(model);
                }
                else
                {
                    TempData["showbtn"] = false;
                    ModelState.AddModelError("PhoneNumber", "Mobile number already exist. ");
                    return View(model);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RegisterVendorAsync(VendorRegister model)
        {
            RepoCommon repoCommon = new RepoCommon();

            if (repoCommon.VerifyOTP(model.code))
            {
                model.Password = "Abc@123$";
                model.ConfirmPassword = "Abc@123$";

                var res = await VendorRegister(model);
                if (res.ResultFlag == true)
                {
                    string usrid = (string)res.Data;
                    var token = UserManager.GeneratePasswordResetToken(usrid);
                    ResetPasswordViewModel reset = new ResetPasswordViewModel
                    {
                        PhoneNumber = model.PhoneNumber,
                        Code = token,
                    };
                    TempData["reset"] = reset;
                    return Redirect(Url.Action("ResetPassword", "Account"));
                    //return RedirectToAction("ResetPassword","Account",reset);
                }
                else
                {
                    return RedirectToAction("MobileVerify", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("code", "Enter valid OTP code");
                return View(model);
            }

        }
        #endregion

        #region Update User Email and Mobile 
        public async Task<ActionResult> UpdateCurrentUserEmail(clsUserBasicDetails model)
        {
            try
            {
                UserDetail userDetail = new UserDetail();
                bool isUpdate = false;
                Utility util = new Utility();
                RepoUser repoUser = new RepoUser();
                var userByemail = UserManager.FindByEmail(model.Email);
                if (userByemail == null)
                {
                    var current_user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                    current_user.Email = model.Email;
                    var result = await UserManager.UpdateAsync(current_user);
                    if (result.Succeeded)
                    {
                        RepoUser repouser = new RepoUser();
                        isUpdate = repouser.UpdateCurrentUserEmail(model.Email, util.GetCurrentUserId());
                        if (isUpdate)
                        {
                            RepoCookie cookie = new RepoCookie();
                            cookie.AddCookiesValue(CookieName.EmailId, model.Email);
                        }
                    }
                }
                return Json(new ResultModel
                {
                    ResultFlag = isUpdate,
                    Data = "",
                    Message = isUpdate == true ? "User email update successfully!" : "Email already exist!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> UpdateCurrentUserMobile(clsUserBasicDetails model)
        {
            try
            {
                UserDetail userDetail = new UserDetail();
                bool isUpdate = false;
                var user = UserManager.FindByName(model.Mobile);
                if (user == null)
                {
                    var current_user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                    current_user.UserName = model.Mobile;

                    Utility util = new Utility();
                    var result = await UserManager.UpdateAsync(current_user);
                    if (result.Succeeded)
                    {
                        RepoUser repouser = new RepoUser();
                        isUpdate = repouser.UpdateCurrentUserMobile(model.Mobile, util.GetCurrentUserId());
                        if (isUpdate)
                        {
                            RepoCookie cookie = new RepoCookie();
                            cookie.AddCookiesValue(CookieName.Mobile, model.Mobile);
                            cookie.AddCookiesValue(CookieName.UserName, model.Mobile);
                        }
                    }
                }
                return Json(new ResultModel
                {
                    ResultFlag = isUpdate,
                    Data = "",
                    Message = isUpdate == true ? "Username Update successfully!" : "Mobile number already exist!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


    }
}