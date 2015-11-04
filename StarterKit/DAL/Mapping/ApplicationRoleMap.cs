using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.DAL.Mapping
{
    public class ApplicationRoleMap : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleMap()
        {
            // Primary Key
            HasKey(t => new { t.Id, t.TenantId });

            Property(t => t.IsSystem).IsRequired();

            Property(t => t.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_RoleTenant", 1) { IsUnique = true }));
            Property(t => t.TenantId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_RoleTenant", 2) { IsUnique = true }));

            HasRequired(t => t.Tenant).WithMany().WillCascadeOnDelete(false);
            Ignore(t => t.EntityId);
        }
    }
}
