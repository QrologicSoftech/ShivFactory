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

        #region LogIn
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
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
            var result = await LogInApI(model);
            if (result.ResultFlag == true)
            {
                return RedirectToLocal(result.Data, returnUrl);
            }
            else
            {
                ModelState.AddModelError("RememberMe", result.Message);
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
        public async Task<ActionResult> Register(CustomerRegister model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //  var res = await CustomerRegister(model); VendorRegister
            var res = await VendorRegister(model); 
            if (res.ResultFlag == true)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", res.Message);
                return View(model);
            }



            // If we got this far, something failed, redisplay form
            return View(model);
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
            return code == null ? View("Error") : View();
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
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
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
        public async Task<ResultModel> VendorRegister(CustomerRegister model)
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
                        UserManager.AddToRole(user.Id, UserRoles.Vendor);

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

        #region Customer Register Api
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultModel> CustomerRegister(CustomerRegister model)
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
                        if (!user.EmailConfirmed)
                        {
                            message = "Email not confirmed.";
                        }
                        else
                        {
                            RepoUser ru = new RepoUser();
                            var userDetails = ru.GetUserDetailsBYUserId(user.Id);
                            var role = UserManager.GetRoles(user.Id).FirstOrDefault();
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
                                Mobile = userDetails.Mobile
                            };

                            // add authontication
                            ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                            identity.AddClaim(new Claim(ClaimTypes.Role, role));
                            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                            AuthenticationManager.SignIn(identity);


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
    }
}