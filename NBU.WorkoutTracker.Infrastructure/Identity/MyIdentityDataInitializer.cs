using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace NBU.WorkoutTracker.Infrastructure.Identity
{
    /// <summary>
    /// Used to seed roles and users
    /// </summary>
    public static class MyIdentityDataInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                var role = new IdentityRole();
                role.Name = "user";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                var role = new IdentityRole();
                role.Name = "admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("user3@abv.bg").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "user3@abv.bg";
                user.Email = "user3@abv.bg";

                IdentityResult result = userManager.CreateAsync(user, "Pass1234%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "user").Wait();
                }
            }


            if (userManager.FindByNameAsync("admin2@abv.bg").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin2@abv.bg";
                user.Email = "admin2@abv.bg";

                IdentityResult result = userManager.CreateAsync(user, "Admin1234%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }

        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
    }
}
