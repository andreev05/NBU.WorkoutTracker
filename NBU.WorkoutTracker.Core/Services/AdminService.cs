using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NBU.WorkoutTracker.Core.Contracts;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Core.Services
{
    public class AdminService : IAdminService
    {
        UserManager<ApplicationUser> userManager;


        public AdminService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        public List<UserViewModel> GetUsers()
        {
            using (userManager)
            { 
                var usersAndRoles = userManager.Users.Select(u =>
                    new UserViewModel
                    {
                        Email = u.Email,
                        UserName = u.UserName,
                        PhoneNumber = u.PhoneNumber

                    }
                ).ToList();

                return usersAndRoles;
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
                await userManager.CreateAsync(appUser, vm.Password);
            }
        }
    }
}
