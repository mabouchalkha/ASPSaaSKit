using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.DAL.Mapping
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            // Primary Key
            HasKey<string>(t => t.Id);

            // Properties
            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(100);

            Ignore(t => t.FullName);
            Ignore(t => t.EntityId);
            // Table & Column Mappings

            // Relationships

        }
    }
}
