using Microsoft.AspNet.Identity.Owin;
using StarterKit.Architecture.Abstract;
using StarterKit.DAL;
using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web;

namespace StarterKit.Repositories
{
    [Export(typeof(IRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RoleRepository : GenericTenantableRepository<ApplicationRole, ApplicationDbContext, string>, IRoleRepository
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        protected override DbSet<ApplicationRole> DbSet(ApplicationDbContext entityContext)
        {
            return (DbSet<ApplicationRole>)entityContext.Roles;
        }

        protected override Expression<Func<ApplicationRole, bool>> IdentifierPredicate(ApplicationDbContext entityContext, string id)
        {
            return (e => e.Id == id.ToString());
        }
    }
}