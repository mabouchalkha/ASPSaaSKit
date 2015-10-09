using StarterKit.Architecture.Abstract;
using StarterKit.Architecture.Interfaces;
using StarterKit.DAL;
using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarterKit.Repositories
{
    public class TenantRepository : RepositoryTenantable, IBaseRepository<Tenant, Guid>
    {
        public TenantRepository() :base() { }

        public List<Tenant> Index()
        {
            return context.Tenants.ToList();
        }

        public Tenant Read(Guid id)
        {
            return context.Tenants.FirstOrDefault(c => c.Id == id);
        }

        public bool Create(Tenant entity)
        {
            context.Tenants.Add(entity);
            int changeCount = context.SaveChanges();

            return changeCount > 0;
        }

        public bool Update(Tenant entity)
        {
            return context.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}