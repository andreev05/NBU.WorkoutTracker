using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NBU.WorkoutTracker.Infrastructure.Data.Contexts;
using System.Linq;
using NBU.WorkoutTracker.Core.Services;
using System.Threading.Tasks;

namespace NBU.WorkoutTracker.Tests.Admin
{
    [TestFixture]
    public class AdminServiceTests
    {
        [Test]
        public async Task ShouldReturnUsers()
        {
           //Arrange
            IList<ApplicationUser> fakeUsers = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Email = "user1@abv.bg",
                    UserName = "user1",
                    PhoneNumber = "12345"
                },
                new ApplicationUser()
                {
                    Email = "user2@abv.bg",
                    UserName = "user2",
                    PhoneNumber = "2345"
                }
            };


            var fakeRoles = new List<IdentityRole>()
            {

                new IdentityRole()
                {
                    Name = "user"
                },
                new IdentityRole()
                {
                    Name = "admin"
                }
            }.AsQueryable();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var roleStore = new Mock<IRoleStore<IdentityRole>>();

            var userManagerMoq = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            var roleManagerMoq = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

            roleManagerMoq.Setup(x => x.Roles).Returns(fakeRoles);
            //userManagerMoq.Setup(x => x.Users).Returns(fakeUsers);
            userManagerMoq.Setup(x => x.GetUsersInRoleAsync("user")).Returns(Task.FromResult(fakeUsers));
            
            // Act
            var adminService = new AdminService(userManagerMoq.Object, roleManagerMoq.Object);

            var actualUsersAndRoles = await adminService.GetUsers();


            // Assert
            Assert.AreEqual(actualUsersAndRoles.Count, 2);
        }
    }
}
