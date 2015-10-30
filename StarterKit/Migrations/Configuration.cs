namespace StarterKit.Migrations
{
    using Microsoft.AspNet.Identity;
    using DOM;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<StarterKit.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(StarterKit.DAL.ApplicationDbContext context)
        {
            //ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            //Tenant newTenant = null;

            ////add tenant for dom/mohamed
            //if (context.Tenants.Count() == 0)
            //{
            //    newTenant = context.Tenants.Add(new Tenant() { IsTrial = false, ActiveUntil = DateTime.Now.AddDays(15) });
            //}
            //else
            //{
            //    newTenant = context.Tenants.First();
            //}

            //// check if roles are present
            //if (roleManager.Roles.Count() == 0)
            //{
            //    roleManager.Create(new ApplicationRole("Admin", newTenant.Id));
            //    roleManager.Create(new ApplicationRole("Owner", newTenant.Id));
            //    roleManager.Create(new ApplicationRole("User", newTenant.Id));
            //    roleManager.Create(new ApplicationRole("Reader", newTenant.Id));
            //}

            //// check if dom' user is present
            //if (manager.Users.FirstOrDefault(u => u.Email == "dom.dlapointe@gmail.com") == null)
            //{
            //    var user = new ApplicationUser()
            //    {
            //        Email = "dom.dlapointe@gmail.com",
            //        UserName = "dom.dlapointe@gmail.com",
            //        EmailConfirmed = true,
            //        TenantId = newTenant.Id,
            //        Tenant = newTenant,
            //        FirstName = "Dom",
            //        LastName = "Lap"
            //    };

            //    var result = manager.Create(user, "!Q2w3e4r");

            //    if (result.Succeeded)
            //    {
            //        manager.AddToRole(user.Id, "Admin");
            //    }
            //}

            //// check if mohamed's user is present
            //if (manager.Users.FirstOrDefault(u => u.Email == "mabouchalkha@gmail.com") == null)
            //{
            //    var user = new ApplicationUser()
            //    {
            //        Email = "mabouchalkha@gmail.com",
            //        UserName = "mabouchalkha@gmail.com",
            //        EmailConfirmed = true,
            //        TenantId = newTenant.Id,
            //        Tenant = newTenant,
            //        FirstName = "Moh",
            //        LastName = "Bou"
            //    };

            //    var result = manager.Create(user, "!Q2w3e4r");

            //    if (result.Succeeded)
            //    {
            //        manager.AddToRole(user.Id, "Admin");
            //    }
            //}
        }
    }
}
