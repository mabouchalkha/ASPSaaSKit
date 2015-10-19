using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.DAL.Mapping
{
    class StripeEventLogMap : EntityTypeConfiguration<StripeEventLog>
    {
        public StripeEventLogMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties

            Ignore(t => t.EntityId);
            // Table & Column Mappings

            // Relationships
        }
    }
}
