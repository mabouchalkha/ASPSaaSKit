using EntityFramework.DynamicFilters;
using StarterKit.DAL;
using StarterKit.Helpers;

namespace StarterKit.Architecture.Abstract
{
    // NEVER USE A CONTEXT OUTSIDE OF A REPOSITORY... IF WE DO, TENANTID WILL BE IGNORE AND DATA WILL LEAK
    public abstract class RepositoryTenantable
    {
        protected ApplicationDbContext context;

        protected RepositoryTenantable()
        {
            context = new ApplicationDbContext();
            context.EnableFilter("Tenant");
            context.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());
        }
    }
}