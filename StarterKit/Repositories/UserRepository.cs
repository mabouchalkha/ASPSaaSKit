using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StarterKit.Architecture.Abstract;
using StarterKit.Architecture.Exceptions;
using StarterKit.DAL;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Repositories.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace StarterKit.Repositories
{
    [Export(typeof(IUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserRepository : GenericTenantableRepository<ApplicationUser, ApplicationDbContext, string>, IUserRepository
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        protected override DbSet<ApplicationUser> DbSet(ApplicationDbContext entityContext)
        {
            return (DbSet<ApplicationUser>)entityContext.Users;
        }

        protected override Expression<Func<ApplicationUser, bool>> IdentifierPredicate(ApplicationDbContext entityContext, string id)
        {
            return (e => e.Id == id);
        }

        public async Task<IdentityResult> ValidateUser(ApplicationUser entity)
        {
            return await UserManager.UserValidator.ValidateAsync(entity);
        }

        public bool EmailExit(string email)
        {
            using (ApplicationDbContext entityContext = this.GetContext())
            {
                ApplicationUser user = entityContext.Users.FirstOrDefault(u => u.Email == email);
                return user != null;
            }
        }

        public override ApplicationUser Create(ApplicationUser entity)
        {
            using (ApplicationDbContext entityContext = this.GetContext())
            {
                entityContext.Users.Add(entity);
                var changeCount = entityContext.SaveChanges();

                if (changeCount > 0)
                {
                    string token = UserManager.GenerateEmailConfirmationToken(entity.Id);
                    token = HttpUtility.UrlEncode(token);
                    UserManager.SendEmail(entity.Id, "Confirm Email", "Please confirm your email following this link : " + HttpContext.Current.Request.UrlReferrer + "#/confirmemail?userid=" + entity.Id + "&code=" + token);
                    UserManager.AddToRole(entity.Id, "User");

                    return entity;
                }

                return entity;
            }
        }

        public override ApplicationUser Update(ApplicationUser entity)
        {
            using (ApplicationDbContext entityContext = this.GetContext())
            {
                this.ValidateTenant(entity);

                ApplicationUser databaseUser = this.Read(entity.Id);

                if (databaseUser == null)
                {
                    throw new ApplicationException(string.Format("Canont find user with id {0}", entity.Id));
                }
                else
                {
                    if (!databaseUser.Email.Equals(entity.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        databaseUser.EmailConfirmed = false;
                        string token = UserManager.GenerateEmailConfirmationToken(entity.Id);
                        token = HttpUtility.UrlEncode(token);
                        UserManager.SendEmail(entity.Id, "Confirm Email", "Please confirm your email following this link : " + HttpContext.Current.Request.UrlReferrer + "#/confirmemail?userid=" + entity.Id + "&code=" + token);
                    }

                    databaseUser.FirstName = entity.FirstName;
                    databaseUser.LastName = entity.LastName;
                    databaseUser.Email = entity.Email;
                }

                return this.UpdateGeneric(entityContext, databaseUser);
            }
        }

        public override void Delete(ApplicationUser entity)
        {
            using (ApplicationDbContext entityContext = this.GetContext())
            {
                this.ValidateTenant(entity);

                ApplicationUser currentUser = UserHelper.GetCurrentUser();

                if (currentUser.Id == entity.Id)
                {
                    throw new ApplicationException(App_GlobalResources.lang.userDeleteSelf);
                }

                UserManager.RemoveFromRoles(entity.Id, UserManager.GetRoles(entity.Id).ToArray());

                base.DeleteGeneric(entityContext, entity);
            }
        }
    }
}