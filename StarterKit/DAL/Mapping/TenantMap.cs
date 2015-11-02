using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            HasKey(t => t.TenantId);

            // Set Id as GUID
            Property(t => t.TenantId)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Properties
            Property(t => t.Name)
                .IsOptional()
                .HasMaxLength(100);

            Property(t => t.StripeCustomerId)
                .IsOptional()
                .HasMaxLength(500);

            Ignore(t => t.EntityId);
        }
    }
}
