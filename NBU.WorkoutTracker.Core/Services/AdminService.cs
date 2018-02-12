using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NBU.WorkoutTracker.Core.Contracts;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Core.Services
{
    public class AdminService : IAdminService
    {
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;


        public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Gets users and their role. In this case the user has only one role.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserViewModel>> GetUsers()
        {

            using (userManager)
            using (roleManager)
            {
                var roles = roleManager.Roles;
                List<UserViewModel> results = new List<UserViewModel>();
                foreach(var role in roles)
                { 
                    
                    var usersAndRoles = await userManager.GetUsersInRoleAsync(role.Name);

                    if(usersAndRoles != null)
                    { 
                        results.AddRange(usersAndRoles.Select(u =>
                            new UserViewModel
                            {
                                Email = u.Email,
                                UserName = u.UserName,
                                PhoneNumber = u.PhoneNumber,
                                Role = role.Name
                            }
                        ).ToList());
                    }
                }


                return results;
            }
        }

        public async Task CreateUser(CreateUserViewModel vm)
        {
            var appUser = new ApplicationUser() {
                Email = vm.Email,
                UserName = vm.UserName
            };

            using (userManager)
            { 
                var result = await userManager.CreateAsync(appUser, vm.Password);

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, vm.Role);
                }
            }
        }
    }
}
