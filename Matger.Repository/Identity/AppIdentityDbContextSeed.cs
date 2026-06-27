using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;

namespace Matger.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsynk(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Mohamed Nasser",
                    Email = "mohamednasser13200@gmail.com",
                    UserName = "mohamednasser13200",
                    PhoneNumber = "01100284237"
                };
                await userManager.CreateAsync(User, "P@$$w0rd");
            }
        }
    }
}
