using StarterKit.DOM;
using System;
using System.Collections.Generic;
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
            HasKey<string>(t => t.Id);

            //HasRequired(t => t.Tenant).WithMany().WillCascadeOnDelete(false);

            Ignore(t => t.EntityId);
        }
    }
}
