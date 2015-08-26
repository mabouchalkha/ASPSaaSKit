using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StarterKit.DAL;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using System.Threading.Tasks;

namespace StarterKit.Helpers
{
    public static class UserHelper
    {
        public static ApplicationUser GetCurrentUser()
        {
            var user = HttpContext.Current != null && HttpContext.Current.User != null ? HttpContext.Current.User.Identity : null;

            if (user != null)
            {
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var currentUser = userManager.FindById(user.GetUserId());

                if (currentUser != null) 
                {
                    return currentUser;                
                }
                 
            }

            return null;
        }

        public async static Task SendEmailConfirmationAsync(ApplicationUserManager manager, string referrer, string id)
        {
            string token = await manager.GenerateEmailConfirmationTokenAsync(id);
            token = HttpUtility.UrlEncode(token);
            await manager.SendEmailAsync(id, "Confirm Email", "Please confirm your email following this link : " + referrer + "#/confirmemail?userid=" + id + "&code=" + token);
        }
    }
}