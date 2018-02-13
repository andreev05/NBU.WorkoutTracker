using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NBU.WorkoutTracker.Infrastructure.Identity;
using NBU.WorkoutTracker.Core.Contracts;
using NBU.WorkoutTracker.Core.ViewModels;

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
            var user = await userManager.GetUserAsync(HttpContext.User);
            var completedWorkouts = workoutHistoryService.GetUserWorkoutHistory(user.Id);
            return View(completedWorkouts);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int workoutId)
        {
            CreateCompletedWorkoutViewModel vm = new CreateCompletedWorkoutViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (!workoutHistoryService.CheckUserId(user.Id, workoutId))
            {
                return NotFound();
            }

            var workout =
            vm.WorkoutId = workoutId;

            vm.DetailedExercises = workoutHistoryService.GetWorkoutExercises(user.Id, workoutId).ToList();
            return View(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCompletedWorkoutViewModel vm)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            workoutHistoryService.AddCompletedWorkout(user.Id, vm);
            return View(nameof(Index));
        }

        private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    workoutHistoryService.Dispose();
                    base.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}