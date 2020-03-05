using Golobal_IMC_Task.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "ahmed1@CA", Email = "ahmed1@CA" };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "ahmed1@CA");
            }
        }
    }
}
