using StarterKit.DAL;

namespace StarterKit.Architecture.Abstract
{
    public abstract class RepositoryNotTenantable 
    {
        protected ApplicationDbContext context;

        protected RepositoryNotTenantable()
        {
            this.context = new ApplicationDbContext();
            //context.DisableFilter("Tenant");
        }
    }
}