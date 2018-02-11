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

namespace NBU.WorkoutTracker.Tests.Admin
{
    [TestFixture]
    public class AdminServiceTests
    {
        [Test]
        public void ShouldReturnUsersAndRoles()
        {
           //Arrange
            var fakeUsers = new List<ApplicationUser>()
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
            }.AsQueryable();


            var fakeRoles = new List<IdentityRole>()
            {

                new IdentityRole()
                {
                    Name = "role1"
                },
                new IdentityRole()
                {
                    Name = "role2"
                }
            }.AsQueryable();

            Mock<UserManager<ApplicationUser>> userManagerMoq = new Mock<UserManager<ApplicationUser>>();
            userManagerMoq.Setup(x => x.Users).Returns(fakeUsers);

            // Act
            var adminService = new AdminService(userManagerMoq.Object);
            var actualUsersAndRoles = adminService.GetUsers();


            // Assert
            Assert.Equals(actualUsersAndRoles.Count, 2);
        }
    }
}
