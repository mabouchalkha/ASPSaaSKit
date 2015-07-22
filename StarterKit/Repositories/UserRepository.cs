using StarterKit.Architecture.Abstract;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using System.Collections.Generic;
using System.Linq;

namespace StarterKit.Repositories
{
    public class UserRepository : RepositoryTenantable<ApplicationUser>, IRepository<ApplicationUser, string>
    {
        public List<ApplicationUser> Index()
        {
            return context.Users.ToList();
        }

        public ApplicationUser Read(string id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser Create(ApplicationUser entity)
        {
            context.Users.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public bool Update(ApplicationUser entity)
        {
            return context.SaveChanges() > 0;
        }

        public bool HasPendingChange(ApplicationUser entity)
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}