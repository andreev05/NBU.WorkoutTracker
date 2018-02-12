using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NBU.WorkoutTracker.Infrastructure.Identity;
using NBU.WorkoutTracker.Core.Contracts;

namespace NBU.WorkoutTracker.Controllers
{
    public class WorkoutHistoryController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkoutHistory workoutHistoryService;

        public WorkoutHistoryController(UserManager<ApplicationUser> userManager, IWorkoutHistory workoutHistoryService)
        {
            this.userManager = userManager;
            this.workoutHistoryService = workoutHistoryService;
        }


        public IActionResult Index()
        {
            //workoutHistoryService.GetUserWorkoutHistory(userId);
            return View();
        }
    }
}