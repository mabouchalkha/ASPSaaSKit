using EntityFramework.DynamicFilters;
using StarterKit.Architecture.Abstract;
using StarterKit.Architecture.Interfaces;
using StarterKit.DAL;
using StarterKit.DOM;
using StarterKit.Helpers;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq.Expressions;

namespace StarterKit.Repositories
{
    [Export(typeof(ITenantRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TenantRepository : TenantableBaseRepository<Tenant, Guid>, ITenantRepository
    {
        protected override void ActivateTenantCRUD(ApplicationDbContext entityContext)
        {
            entityContext.EnableFilter("Tenant");
            entityContext.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());
        }
        
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

//public TenantRepository() :base() { }

//public List<Tenant> Index()
//{
//    return context.Tenants.ToList();
//}

//public Tenant Read(Guid id)
//{
//    return context.Tenants.FirstOrDefault(c => c.Id == id);
//}

//public bool Create(Tenant entity)
//{
//    context.Tenants.Add(entity);
//    int changeCount = context.SaveChanges();

//    return changeCount > 0;
//}

//public bool Update(Tenant entity)
//{
//    return context.SaveChanges() > 0;
//}

//public bool Delete(Guid id)
//{
//    throw new NotImplementedException();
//}