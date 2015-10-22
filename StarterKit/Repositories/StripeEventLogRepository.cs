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
    [Export(typeof(IStripeEventLogRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StripeEventLogRepository : GenericRepository<StripeEventLog, ApplicationDbContext, int>, IStripeEventLogRepository
    {
        protected override DbSet<StripeEventLog> DbSet(ApplicationDbContext entityContext)
        {
            return entityContext.StripeEventLogs;
        }

        protected override Expression<Func<StripeEventLog, bool>> IdentifierPredicate(ApplicationDbContext entityContext, int id)
        {
            return (e => e.Id == id);
        }
    }
}
