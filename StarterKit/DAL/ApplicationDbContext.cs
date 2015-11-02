using EntityFramework.DynamicFilters;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using StarterKit.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using StarterKit.DAL.Mapping;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StarterKit.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnections")
        {
            Database.Initialize(false);
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<StripeEventLog> StripeEventLogs { get; set; }
        //public DbSet<ApplicationRole> Roles { get; set; }

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

            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Configurations.Add(new TenantMap());
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new ApplicationRoleMap());
            modelBuilder.Configurations.Add(new SubscriptionMap());
            modelBuilder.Configurations.Add(new SubscriptionPlanMap());
            modelBuilder.Configurations.Add(new FeatureMap());
            modelBuilder.Configurations.Add(new StripeEventLogMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries();

            if (changeSet != null)
            {
                Guid currentTenantId = TenantHelper.GetCurrentTenantId();

                foreach (var history in changeSet.Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added ||
                           e.State == EntityState.Modified))
                    .Select(e => e.Entity as IModificationHistory)
                   )
                {
                    history.UpdateDate = DateTime.Now;
                    //history.UpdatedBy = 
                    if (history.CreatedDate == DateTime.MinValue)
                    {
                        //history.CreatedBy = 
                        history.CreatedDate = DateTime.Now;
                    }
                }

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