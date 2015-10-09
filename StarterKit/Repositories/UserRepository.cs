using StarterKit.Architecture.Abstract;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Security;
using StarterKit.DAL;

namespace StarterKit.Repositories
{
    public class UserRepository : RepositoryTenantable, IBaseRepository<ApplicationUser, string>
    {
        private ApplicationUserManager _userManager;

        public UserRepository() : base() { }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public List<ApplicationUser> Index()
        {
            return context.Users.Where(u => u.EmailConfirmed == true).ToList();
        }

        public ApplicationUser Read(string id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool Create(ApplicationUser entity)
        {
            context.Users.Add(entity);
            var changeCount = context.SaveChanges();

            if (changeCount > 0)
            {
                string token = UserManager.GenerateEmailConfirmationToken(entity.Id);
                token = HttpUtility.UrlEncode(token);
                UserManager.SendEmail(entity.Id, "Confirm Email", "Please confirm your email following this link : " + HttpContext.Current.Request.UrlReferrer + "#/confirmemail?userid=" + entity.Id + "&code=" + token);
                UserManager.AddToRole(entity.Id, "User");

                return true;
            }

            return false;
        }

        public async Task<IdentityResult> ValidateUser(ApplicationUser entity)
        {
            return await UserManager.UserValidator.ValidateAsync(entity);
        }

        public bool Update(ApplicationUser entity)
        {
            return context.SaveChanges() > 0;
        }

        public bool HasPendingChange(ApplicationUser entity)
        {
            return context.ChangeTracker.HasChanges();
        }

        public bool Delete(string id)
        {
            ApplicationUser userToDelete = context.Users.FirstOrDefault(u => u.Id == id);

            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                return context.SaveChanges() > 0;
            }

            return false;
        }

        public bool EmailExit(string email)
        {
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Email == email);

            return user != null;
        }
    }
}