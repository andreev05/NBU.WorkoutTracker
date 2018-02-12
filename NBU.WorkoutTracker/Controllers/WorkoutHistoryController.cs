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


        public async Task<IActionResult> Index()
        {
            using (userManager)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var completedWorkouts = workoutHistoryService.GetUserWorkoutHistory(user.Id);
                return View(completedWorkouts);
            }
        }

        public async Task<IActionResult> Create()
        {
            using (userManager)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var completedWorkouts = workoutHistoryService.AddCompletedWorkout(user.Id, id);
                return View(completedWorkouts);
            }
        }
    }
}