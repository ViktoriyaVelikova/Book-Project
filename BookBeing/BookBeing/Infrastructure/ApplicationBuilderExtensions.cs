using System;
using System.Linq;
using System.Threading.Tasks;
using BookBeing.Data;
using BookBeing.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static BookBeing.WebConstants;

namespace BookBeing.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<BookBeingDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<BookBeingDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
               new Category{ Name="Fantasy"},
                new Category{ Name="Mystery" },
                new Category{ Name="Thriller" },
                new Category{ Name="Romance" },
                new Category{ Name="Dystopian" },
                new Category{ Name="Contemporary" }
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    var existUserWithRole = await userManager.GetUsersInRoleAsync(AdministratorRoleName);
                    var existRole = await roleManager.RoleExistsAsync(AdministratorRoleName);
                    if (existRole && existUserWithRole != null)
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@bbb.com";
                    const string adminPassword = "123456";

                    var user = new ApplicationUser
                    {
                        Email = adminEmail,
                        Name = adminEmail,
                        UserName = adminEmail
                    };

                    var res = await userManager.CreateAsync(user, adminPassword);

                    var res2 = await userManager.AddToRoleAsync(user, role.Name);

                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
