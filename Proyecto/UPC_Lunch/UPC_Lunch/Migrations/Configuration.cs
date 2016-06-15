namespace UPC_Lunch.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using UPC_Lunch.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UPC_Lunch.Models.UPCLunchContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private void AddUserAndRoleAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // Create Role
            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }
            var user = userManager.FindByName("admin@admin.com");
            if (user == null)
            {
                // Create User
                user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                userManager.Create(user, "password");
            }
            // Add User To Role
            if (user != null && !userManager.IsInRole(user.Id, "Administrator"))
            {
                userManager.AddToRole(user.Id, "Administrator");
            }

            // Create Restaurant Role
            if (!roleManager.RoleExists("Restaurant"))
            {
                roleManager.Create(new IdentityRole("Restaurant"));
            }
        }

        protected override void Seed(UPC_Lunch.Models.UPCLunchContext context)
        {
            AddUserAndRoleAdmin();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
