using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NBU.WorkoutTracker.Controllers
{
    public class WorkoutHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}