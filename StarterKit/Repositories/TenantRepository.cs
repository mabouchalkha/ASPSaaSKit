using EntityFramework.DynamicFilters;
using StarterKit.Architecture.Abstract;
using StarterKit.Architecture.Interfaces;
using StarterKit.DAL;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Repositories.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq.Expressions;

namespace StarterKit.Repositories
{
    [Export(typeof(ITenantRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TenantRepository : GenericTenantableRepository<Tenant, ApplicationDbContext, Guid>, ITenantRepository
    {
        protected override DbSet<Tenant> DbSet(ApplicationDbContext entityContext)
        {
            return entityContext.Tenants;
        }

        protected override Expression<Func<Tenant, bool>> IdentifierPredicate(ApplicationDbContext entityContext, Guid id)
        {
            return (e => e.Id == id);
        }
    }
}