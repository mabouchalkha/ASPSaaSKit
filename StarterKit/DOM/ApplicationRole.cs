using StarterKit.Architecture.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StarterKit.DOM
{
    public class ApplicationRole : IdentityRole, IIdentifiableTenantableEntity<string>, ITenantable
    {
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }

        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public ApplicationRole(string name, Guid tenantId) : base(name)
        {
            this.TenantId = tenantId;
        }

        public string EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}