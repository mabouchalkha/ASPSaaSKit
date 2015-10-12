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
using System.Data.Entity;
using System.Linq.Expressions;
using EntityFramework.DynamicFilters;
using StarterKit.Helpers;
using System.ComponentModel.Composition;

namespace StarterKit.Repositories
{
    [Export(typeof(IUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserRepository : TenantableBaseRepository<ApplicationUser, string>, IUserRepository
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        //public override IEnumerable<ApplicationUser> Index()
        //{
        //    using (ApplicationDbContext entityContext = new ApplicationDbContext())
        //        return entityContext.Users.Where(u => u.EmailConfirmed == true).ToArray().ToList();
        //}

        //public override ApplicationUser Read(string id)
        //{
        //    using (ApplicationDbContext entityContext = new ApplicationDbContext())
        //        return entityContext.Users.FirstOrDefault(u => u.Id == id);
        //}

        public override ApplicationUser Create(ApplicationUser entity)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
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

        public async Task<IdentityResult> ValidateUser(ApplicationUser entity)
        {
            return await UserManager.UserValidator.ValidateAsync(entity);
        }

        //public override ApplicationUser Update(ApplicationUser entity)
        //{
        //    using (ApplicationDbContext entityContext = new ApplicationDbContext())
        //    {
        //        entityContext.SaveChanges();
        //        return entity;
        //    }
        //}

        public bool HasPendingChange(ApplicationUser entity)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
                return entityContext.ChangeTracker.HasChanges();
        }

        //public override void Delete(string id)
        //{
        //    using (ApplicationDbContext entityContext = new ApplicationDbContext())
        //    {
        //        ApplicationUser userToDelete = entityContext.Users.FirstOrDefault(u => u.Id == id);

        //        if (userToDelete != null)
        //        {
        //            entityContext.Users.Remove(userToDelete);
        //            entityContext.SaveChanges();
        //        }
        //    }
        //}

        public bool EmailExit(string email)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
            {
                ApplicationUser user = entityContext.Users.FirstOrDefault(u => u.Email == email);
                return user != null;
            }
        }

        protected override DbSet<ApplicationUser> DbSet(ApplicationDbContext entityContext)
        {
            return (DbSet<ApplicationUser>)entityContext.Users;
        }

        protected override Expression<Func<ApplicationUser, bool>> IdentifierPredicate(ApplicationDbContext entityContext, string id)
        {
            return (e => e.Id == id);
        }

        protected override void ActivateTenantCRUD(ApplicationDbContext entityContext)
        {
            entityContext.EnableFilter("Tenant");
            entityContext.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());
        }
    }
}