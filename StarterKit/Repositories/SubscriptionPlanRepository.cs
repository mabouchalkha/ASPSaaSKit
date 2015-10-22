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
    [Export(typeof(ISubscriptionPlanRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SubscriptionPlanRepository : GenericRepository<SubscriptionPlan, ApplicationDbContext, int>, ISubscriptionPlanRepository
    {
        protected override DbSet<SubscriptionPlan> DbSet(ApplicationDbContext entityContext)
        {
            return entityContext.SubscriptionPlans;
        }

        protected override Expression<Func<SubscriptionPlan, bool>> IdentifierPredicate(ApplicationDbContext entityContext, int id)
        {
            return (e => e.Id == id);
        }
    }
}
