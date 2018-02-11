using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NBU.WorkoutTracker.Controllers
{
    /// <summary>
    /// The admin can only view the users and create new admins
    /// </summary>
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}