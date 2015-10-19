using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.DAL.Mapping
{
    public class TenantMap : EntityTypeConfiguration<Tenant>
    {
        public TenantMap()
        {
            // Primary Key
            HasKey<Guid>(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsOptional()
                .HasMaxLength(50);

            Property(t => t.StripeCustomerId)
                .IsOptional()
                .HasMaxLength(500);

            Ignore(t => t.EntityId);
            // Table & Column Mappings

            // Relationships
            HasMany(t => t.ApplicationUser)
                .WithRequired(t => t.Tenant)
                .HasForeignKey(t => t.TenantId);

        }
    }
}
