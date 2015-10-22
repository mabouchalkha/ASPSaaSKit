using StarterKit.Architecture.Abstract;
using StarterKit.DAL;
using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using StarterKit.Repositories.Interfaces;
using System.ComponentModel.Composition;

namespace StarterKit.Repositories
{
    [Export(typeof(ISubscriptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SubscriptionRepository : GenericRepository<Subscription, ApplicationDbContext, int>, ISubscriptionRepository
    {
        protected override DbSet<Subscription> DbSet(ApplicationDbContext entityContext)
        {
            return entityContext.Subscriptions;
        }

        protected override Expression<Func<Subscription, bool>> IdentifierPredicate(ApplicationDbContext entityContext, int id)
        {
            return (e => e.Id == id);
        }
    }
}
