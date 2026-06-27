using Matger.Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Matger.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindUserWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
