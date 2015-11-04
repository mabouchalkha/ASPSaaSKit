using StarterKit.DOM;
using System.Data.Entity.ModelConfiguration;

namespace StarterKit.DAL.Mapping
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            // Primary Key
            HasKey<string>(t => t.Id);

            //HasRequired(t => t.Tenant).WithMany().WillCascadeOnDelete(false);

            // Properties
            Property(t => t.FirstName)
                .HasMaxLength(100);

            Property(t => t.LastName)
                .HasMaxLength(100);

            Ignore(t => t.FullName);
            Ignore(t => t.EntityId);
        }
    }
}
