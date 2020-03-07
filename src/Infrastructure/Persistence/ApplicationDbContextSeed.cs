using Golobal_IMC_Task.Domain.Entities;
using Golobal_IMC_Task.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            var defaultUser = new ApplicationUser { UserName = "ahmed1@CA", Email = "ahmed1@CA" };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "ahmed1@CA");
            }
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        CategoryName = "Food",
                        Products = new List<Product>
                    {
                        new Product { Price = 120,  Title = "Bread"},
                        new Product { Price = 100,  Title = "Butter" },
                        new Product { Price = 20,  Title = "Sugar" },
                    }
                    },
                    new Category
                    {
                        CategoryName = "Drink",
                        Products = new List<Product>
                            {
                            new Product { Price = 30,  Title = "Milk" },
                            new Product { Price = 200,  Title = "Coffee" }
                            }
                    }
                    );

                context.SaveChanges();

            }
        }
    }
}
