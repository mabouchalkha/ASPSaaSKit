using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StarterKit.DOM;
using StarterKit.Repositories;
using StarterKit.Repositories.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Web;
using System.Web.Mvc;

namespace StarterKit.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false )]
    public sealed class AuthorizeSubscriber : FilterAttribute, IAuthorizationFilter
    {
        [Import(typeof(ITenantRepository))]
        ITenantRepository _tenantRepository;

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            ApplicationUserManager userManager = filterContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName(filterContext.HttpContext.User.Identity.Name);

            _tenantRepository = new TenantRepository();
            var tenant = _tenantRepository.FindBy(t => t.OwnerEmail == user.Email);

            if (tenant == null || DateTime.Now > tenant.ActiveUntil)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
