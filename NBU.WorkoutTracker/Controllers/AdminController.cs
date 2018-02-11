using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBU.WorkoutTracker.Core.Contracts;
using NBU.WorkoutTracker.Core.ViewModels;

namespace NBU.WorkoutTracker.Controllers
{
    /// <summary>
    /// The admin can only view the users and create new admins
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult Index()
        {
            var users = adminService.GetUsers();

            return View(users);
        }

        
        public IActionResult CreateUser()
        {
            CreateUserViewModel vm = new CreateUserViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel vm)
        {
            await adminService.CreateUser(vm);

            return View("Index");
        }
    }
}