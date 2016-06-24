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
        }

        private void AddRestaurantUsers(string email)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // Create Role
            if (!roleManager.RoleExists("Restaurant"))
            {
                roleManager.Create(new IdentityRole("Restaurant"));
            }
            var user = userManager.FindByName(email);
            if (user == null)
            {
                // Create User
                user = new ApplicationUser { UserName = email, Email = email };
                userManager.Create(user, "password");
            }
            // Add User To Role
            if (user != null && !userManager.IsInRole(user.Id, "Restaurant"))
            {
                userManager.AddToRole(user.Id, "Restaurant");
            }
        }

        private void AddTestUsers()
        {
            string email = "test@mail.com";
            ApplicationDbContext context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);

            var user = userManager.FindByName(email);
            if (user == null)
            {
                // Create User
                user = new ApplicationUser { UserName = email, Email = email };
                userManager.Create(user, "password");
            }
        }

        protected override void Seed(UPC_Lunch.Models.UPCLunchContext context)
        {
            AddUserAndRoleAdmin();
            AddRestaurantUsers("monteoro@mail.com");
            AddRestaurantUsers("losalamos@mail.com");
            AddRestaurantUsers("puertopalmera@mail.com");
            AddTestUsers();
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
            context.Restaurantes.AddOrUpdate(
                p => p.Email,
                new Restaurante { RazonSocial = "Chifa Monteoro", RUC = "1049839821", Direccion = "Av. Central,Santiago de Surco 15023,Perú", Latitud = "-12.105089", Longitud = "-76.965109", AforoCompleto = 50, Estado = "Activo", Email = "monteoro@mail.com", MesaDisponible = true },
                new Restaurante { RazonSocial = "Chifa Los Alamos", RUC = "1394829403", Direccion = "Distrito de Lima,Perú", Latitud = "-12.104938", Longitud = "-76.964946", AforoCompleto = 50, Estado = "Activo", Email = "losalamos@mail.com", MesaDisponible = true },
                new Restaurante { RazonSocial = "Puerto Palmera", RUC = "2910956832", Direccion = "Primavera 2268,Lima,Perú", Latitud = "-12.104849", Longitud = "-76.964833", AforoCompleto = 50, Estado = "Activo", Email = "puertopalmera@mail.com", MesaDisponible = true }
            );
            context.SaveChanges();
            context.Platos.RemoveRange(context.Platos);
            context.SaveChanges();
            context.Platos.AddOrUpdate(
                p => p.PlatoId,
                new Plato() { Nombre = "Chi Jau Kay", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Lomo Saltado", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Arroz Chaufa", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Combinado", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Aeropuerto", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Kam Lun Wantan", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Sopa Wantan", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Ti Pa Kay", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "monteoro@mail.com").SingleOrDefault() },

                new Plato() { Nombre = "Arroz Grito", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "losalamos@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Arroz Chaufa", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "losalamos@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Aji de Gallina", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "losalamos@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Sopa Seca", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "losalamos@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Locro de Zapallo", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "losalamos@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Chanfainita", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "losalamos@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Papa a la Huancaína", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "losalamos@mail.com").SingleOrDefault() },

                new Plato() { Nombre = "Cebiche", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "puertopalmera@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Arroz con Mariscos", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "puertopalmera@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Atún Saltado", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "puertopalmera@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Parihuela", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "puertopalmera@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Jalea Mixta", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "puertopalmera@mail.com").SingleOrDefault() },
                new Plato() { Nombre = "Cebiche de Conchas Negras", Disponible = true, Restaurante = context.Restaurantes.Where(x => x.Email == "puertopalmera@mail.com").SingleOrDefault() }
                );
        }
    }
}
