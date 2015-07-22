using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StarterKit.DAL;
using StarterKit.Architecture.Interfaces;

namespace StarterKit.Helpers
{
    public static class TenantHelper
    {
        public static Guid GetCurrentTenantId()
        {
            var user = HttpContext.Current != null && HttpContext.Current.User != null ? HttpContext.Current.User.Identity : null;

            if (user != null)
            {
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var currentUser = userManager.FindById(user.GetUserId());

                if (currentUser != null) 
                {
                    return currentUser.TenantId;                
                }
                 
            }

            return Guid.Empty;
        }
    }
}