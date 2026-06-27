using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;

namespace Matger.Core.Services
{
    public interface ITokenSevice
    {
        Task<string> GetTokenSeviceAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
