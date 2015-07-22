namespace StarterKit.Migrations
{
    using StarterKit.DAL;
    using StarterKit.DOM;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StarterKit.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StarterKit.DAL.ApplicationDbContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            Tenant newTenant = null;

            //add tenant for dom/mohamed
            if (context.Tenants.Count() == 0)
            {
                newTenant = context.Tenants.Add(new Tenant() { IsTrial = false });
            }
            else
            {
                newTenant = context.Tenants.First();
            }

            // check if roles are present
            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole("Admin"));
                roleManager.Create(new IdentityRole("Owner"));
                roleManager.Create(new IdentityRole("User"));
                roleManager.Create(new IdentityRole("Reader"));
            }

            // check if dom' user is present
            if (manager.Users.FirstOrDefault(u => u.Email == "dom.dlapointe@gmail.com") == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "dom.dlapointe@gmail.com",
                    UserName = "dom.dlapointe@gmail.com",
                    EmailConfirmed = true,
                    TenantId = newTenant.Id
                };

                var result = manager.Create(user, "!Q2w3e4r");

                if (result.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }

            // check if mohamed's user is present
            if (manager.Users.FirstOrDefault(u => u.Email == "mabouchalkha@gmail.com") == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "mabouchalkha@gmail.com",
                    UserName = "mabouchalkha@gmail.com",
                    EmailConfirmed = true,
                    TenantId = newTenant.Id
                };

                var result = manager.Create(user, "!Q2w3e4r");

                if (result.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
