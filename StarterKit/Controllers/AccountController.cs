using StarterKit.DOM;
using StarterKit.Repositories;
using StarterKit.Utils;
using StarterKit.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private TenantRepository _tenantRepo = new TenantRepository();

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public AccountController() { }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ConfirmTwoFactor(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                if (await SignInManager.HasBeenVerifiedAsync())
                {
                    var result = await SignInManager.TwoFactorSignInAsync("Email Code", code, false, true);

                    if (result == SignInStatus.Success)
                    {
                        var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
                        return success(string.Empty , user);
                    }
                    else
                    {
                        return unsuccess(string.Empty);
                    }

                }
            }

            return unsuccess(string.Empty);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ConfirmResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ResetPasswordAsync(viewModel.Id, viewModel.Code, viewModel.Password);

                return result.Succeeded == true ? success(string.Empty) : unsuccess(string.Empty);
            }

            return unsuccess(string.Empty);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ResetPassword(ForgotViewModel forgotViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(forgotViewModel.Email);

                if (user != null)
                {
                    string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    token = HttpUtility.UrlEncode(token);
                    await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password following this link : " + Request.UrlReferrer + "#/resetPassword?userid=" + user.Id + "&code=" + token);
                }

                return unsuccess(ErrorUtil.DefaultError);
            }

            return unsuccess(ErrorUtil.DefaultError);
        }
        
        [HttpGet]
        public void Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCurrentUser ()
        {
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            
            return success(string.Empty, new { isAuthenticated = currentUser != null, user = currentUser });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ResendTwoFactor ()
        {
            if (await SignInManager.HasBeenVerifiedAsync())
            {
                var result = await SignInManager.SendTwoFactorCodeAsync("Email Code");
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user != null && user.EmailConfirmed == true)
                {
                    var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, true);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            return success("You are now logged in StarterKit!", user);
                        case SignInStatus.LockedOut:
                            return unsuccess(string.Empty);
                        case SignInStatus.RequiresVerification: //two factor auth... (not activated for now)
                            await SignInManager.SendTwoFactorCodeAsync("Email Code");
                            return unsuccess(string.Empty, null, new { needTwoFactor = true });
                        default:
                            return unsuccess(string.Empty);
                    }
                }
            }

            return unsuccess(string.Empty, JsonStatus.s_401);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ConfirmPassword (ConfirmPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ConfirmEmailAsync(model.id, model.Code);

                return result.Succeeded == true ? success(string.Empty) : unsuccess(string.Empty);
            }

            return unsuccess(string.Empty);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> register (RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                
                IdentityResult userIsValid = await UserManager.UserValidator.ValidateAsync(user);

                if (userIsValid.Succeeded == true)
                {
                    var tenant = _tenantRepo.Create(new DOM.Tenant() { IsTrial = true });
                    user.TenantId = tenant.Id;

                    IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded) return unsuccess(string.Empty);

                    UserManager.AddToRole(user.Id, "Owner");
                    var currentUser = await UserManager.FindByEmailAsync(model.Email);

                    if (currentUser != null)
                    {
                        string token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        token = HttpUtility.UrlEncode(token);
                        await UserManager.SendEmailAsync(user.Id, "Confirm Password", "Please confirm your password following this link : " + Request.UrlReferrer + "#/confirmpassword?userid=" + user.Id + "&code=" + token);

                        return success("Account successfully created");
                    }
                }

                return Json(new { success = false });
            }

            return unsuccess(string.Empty);
        }
	}
}