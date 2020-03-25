namespace FileManagementSystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FileManagementSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FileManagementSystem.Models.ApplicationDbContext context)
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            //1st Migration
            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Data Entry" });
                // Admin

                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "Admin",
                    Email = "admin@backofficeme.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "BO",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                manager.Create(user, "Admin12345*");

                var adminUser = manager.FindByName("Admin");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin" });

                var deuser = new ApplicationUser()
                {
                    UserName = "dataentry",
                    Email = "dataentry@backofficeme.com",
                    EmailConfirmed = true,
                    FirstName = "DataEntry",
                    LastName = "BO",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                manager.Create(deuser, "Admin12345*");

                var deUser1 = manager.FindByName("dataentry");

                manager.AddToRoles(deUser1.Id, new string[] { "Data Entry" });

            }
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
