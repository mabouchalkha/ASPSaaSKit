using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StarterKit.Architecture.Bases;
using StarterKit.Architecture.Interfaces;
using StarterKit.Authorize;
using StarterKit.Business_Engine.Interfaces;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Repositories;
using StarterKit.Repositories.Interfaces;
using StarterKit.Utils;
using StarterKit.ViewModels;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private IBusinessEngineFactory _BusinessEngineFactory;
        private IDataRepositoryFactory _DataRepositoryFactory;

        [ImportingConstructor]
        public AccountController(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
            _DataRepositoryFactory = dataRepositoryFactory;
        }

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
                        return success(string.Empty, user);
                    }
                    else
                    {
                        return unsuccess(ErrorUtil.DefaultError);
                    }

                }
            }

            return unsuccess(ErrorUtil.DefaultError);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ConfirmResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ResetPasswordAsync(viewModel.Id, viewModel.Code, viewModel.Password);

                return result.Succeeded == true ? success(App_GlobalResources.lang.passwordReseted) : unsuccess(ErrorUtil.JoinErrors(result.Errors));
            }

            return unsuccess(ErrorUtil.DefaultError);
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

                    return success(App_GlobalResources.lang.passwordReseted);
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
        public JsonResult GetCurrentUser()
        {
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            
            return success(string.Empty, new { isAuthenticated = currentUser != null, user = currentUser });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ResendTwoFactor()
        {
            if (await SignInManager.HasBeenVerifiedAsync())
            {
                var result = await SignInManager.SendTwoFactorCodeAsync("Email Code");
                return success(App_GlobalResources.lang.resendTwoFactor);
            }

            return unsuccess(ErrorUtil.DefaultError);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (user.EmailConfirmed == false)
                    {
                        await UserHelper.SendEmailConfirmationAsync(UserManager, Request.UrlReferrer.ToString(), user.Id);
                        return info(App_GlobalResources.lang.loginConfirmEmail, null, new { needEmailConfirmation = true });
                    }

                    var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, true);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            return success(App_GlobalResources.lang.loggedIn, user);
                        case SignInStatus.LockedOut:
                            return unsuccess(ErrorUtil.DefaultError);
                        case SignInStatus.RequiresVerification: //two factor auth... (not activated for now)
                            await SignInManager.SendTwoFactorCodeAsync("Email Code");
                            return success(App_GlobalResources.lang.twoFactorEnable, null, new { needTwoFactor = true });
                        default:
                            return unsuccess(ErrorUtil.DefaultError);
                    }
                }
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState), JsonStatus.s_401);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ConfirmEmail(ConfirmEmail model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ConfirmEmailAsync(model.id, model.Code);
                var user = await UserManager.FindByIdAsync(model.id);
                
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(user.PasswordHash))
                    {
                        string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                        token = HttpUtility.UrlEncode(token);
                        await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password following this link : " + Request.UrlReferrer + "#/resetPassword?userid=" + user.Id + "&code=" + token);

                        return success(App_GlobalResources.lang.emailConfirmedNeedPassword);
                    }

                    return success(App_GlobalResources.lang.emailConfirmed);
                }

                return unsuccess(string.Join("<br />", result.Errors));
            }

            return unsuccess(ErrorUtil.DefaultError);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                
                IdentityResult userIsValid = await UserManager.UserValidator.ValidateAsync(user);

                if (userIsValid.Succeeded == true)
                {
                    IGlobalTenantRepository tenantRepository = _DataRepositoryFactory.GetDataRepository<IGlobalTenantRepository>();

                    using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                    Tenant newTenant = new DOM.Tenant()
                    {
                        IsTrial = true,
                            ActiveUntil = DateTime.Now.AddDays(-1),
                            OwnerEmail = user.Email,
                            OwnerId = user.Id
                    };
                          
                    _tenantRepository.Create(newTenant);
                    user.TenantId = newTenant.TenantId;

                    IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded) return unsuccess(ErrorUtil.JoinErrors(result.Errors));

                    UserManager.AddToRole(user.Id, "Owner");

                        scope.Complete();
                    }

                    //var currentUser = await UserManager.FindByEmailAsync(model.Email);
                    var tenant = tenantRepository.FindBy(t => t.OwnerEmail == user.Email);

                    if (tenant != null)
                    {
                        ISubscriptionEngine subscriptionEngine = _BusinessEngineFactory.GetBusinessEngine<ISubscriptionEngine>();

                        subscriptionEngine.SubscribeTenant(tenant, 3, string.Empty);
                        await UserHelper.SendEmailConfirmationAsync(UserManager, Request.UrlReferrer.ToString(), user.Id);
                        return success(App_GlobalResources.lang.accountCreated);
                    }
                }

                return unsuccess(ErrorUtil.JoinErrors(userIsValid.Errors));
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState), JsonStatus.s_401);
        }
	}
}