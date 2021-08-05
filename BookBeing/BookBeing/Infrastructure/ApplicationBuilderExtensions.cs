using BookBeing.Data;
using BookBeing.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<BookBeingDbContext>();
            data.Database.Migrate();
            SeedCategories(data);
            return app;
        }

        private static void SeedCategories(BookBeingDbContext data)
        {
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
    }
}
