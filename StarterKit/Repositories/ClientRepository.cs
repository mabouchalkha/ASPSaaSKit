using StarterKit.Architecture.Abstract;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using System.Collections.Generic;
using System.Linq;

namespace StarterKit.Repositories
{
    public class ClientRepository : RepositoryTenantable<Client>, IRepository<Client, int>
    {
        public List<Client> Index()
        {
            return context.Clients.ToList();
        }

        public Client Read(int id)
        {
            return context.Clients.FirstOrDefault(c => c.Id == id);
        }

        public Client Create(Client entity)
        {
            context.Clients.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public bool Update(Client entity)
        {
            throw new System.NotImplementedException();
        }
    }
}