using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.DAL.Mapping
{
    public class SubscriptionPlanMap : EntityTypeConfiguration<SubscriptionPlan>
    {
        public SubscriptionPlanMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties

            Ignore(t => t.AmountInDollars);
            Ignore(t => t.EntityId);
            // Table & Column Mappings

            // Relationships
            HasMany(t => t.Feature)
                .WithRequired(t => t.SubscriptionPlan)
                .HasForeignKey(t => t.SubscriptionPlanId);
        }
    }
}
