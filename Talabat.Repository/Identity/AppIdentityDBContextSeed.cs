using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDBContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Gehad Mustafa",
                    Email = "gehad_mustafa00@gmail.com",
                    UserName = "Gehad_Mustafa",
                    PhoneNumber = "01119499124"
                };
                await userManager.CreateAsync(User , "123!@#wwwWWW");
            }
        }
    }
}
