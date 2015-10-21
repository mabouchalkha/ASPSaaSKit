using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StarterKit.DOM;
using System;
using System.Web;
using System.Web.Mvc;

namespace StarterKit.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false )]
    public sealed class AuthorizeSubscriber : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // Maybe use IUserRepository
            ApplicationUserManager userManager = filterContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByName(filterContext.HttpContext.User.Identity.Name);

            if (user == null || DateTime.Now > user.Tenant.ActiveUntil)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
