using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShivFactory.Models;
using ShivFactory.Business.Model;
using ShivFactory.Business.Repository;
using ShivFactory.Models.Other;
using DataLibrary.DL;

namespace ShivFactory.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        #region Services
        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        #region Microsoft Apis

        #region Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }
        #endregion

        #region RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }
        #endregion

        #region AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }
        #endregion

        #region  EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }
        #endregion

        #region VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }
        #endregion

        #region RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }
        #endregion

        #region ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }
        #endregion

        #region SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }


        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion

        #endregion

        #region Accounts Apis

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

                    var user = new ApplicationUser { UserName = model.PhoneNumber, Email = model.EmailId };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var role = await UserManager.AddToRoleAsync(user.Id, UserRoles.Admin);
                        var userDetails = new UserDetail()
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
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
                        var role = await UserManager.AddToRoleAsync(user.Id, UserRoles.Vendor);
                        var userDetails = new UserDetail()
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
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
        [AllowAnonymous]
        [HttpPost]
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
                        var role = await UserManager.AddToRoleAsync(user.Id, UserRoles.Customer);
                        var userDetails = new UserDetail()
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
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
        [HttpPost]
        public async Task<ResultModel> LogIn(LogInModel model)
        {
            try
            {


                string message = "";

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
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
                        message = "Some unexpected error, Please try again.";
                        break;
                    default:
                        message = "Invalid login attempt.";
                        break;
                }

                if (result == SignInStatus.Success)
                {

                    var user = UserManager.FindByName(model.PhoneNumber);
                    if (!user.EmailConfirmed)
                    {
                        message = "Email not confirmed.";
                    }
                    else
                    {

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
    }
}