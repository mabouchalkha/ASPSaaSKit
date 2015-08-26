using EntityFramework.DynamicFilters;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using StarterKit.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace StarterKit.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnections")
        {
            Database.Initialize(false);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public static ApplicationDbContext Create()
        {
            var context = new ApplicationDbContext();
            context.Database.Log = message => Trace.WriteLine(message);
            return context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("Tenant", (ITenantable e, Guid currentTenantId) => e.TenantId == currentTenantId, null);
            modelBuilder.DisableFilterGlobally("Tenant");
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries();

            if (changeSet != null)
            {
                Guid currentTenantId = TenantHelper.GetCurrentTenantId();

                foreach (var entry in changeSet.Where(c => c.State == EntityState.Added || c.State == EntityState.Modified))
                {
                    bool isTenantable = entry.Entity.GetType().GetInterface("ITenantable") != null;

                    if (isTenantable == true)
                    {
                        entry.Property("TenantId").CurrentValue = currentTenantId;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}