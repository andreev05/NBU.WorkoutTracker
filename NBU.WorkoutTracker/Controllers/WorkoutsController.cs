using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Data.Models;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IRDBERepository<Workout> workoutsRepo;
        private readonly IRDBERepository<Exercise> exercisesRepo;
        private readonly IRDBERepository<WorkoutExercise> weRepo;
        private readonly UserManager<ApplicationUser> userManager;


        public WorkoutsController(IRDBERepository<Workout> workoutsRepo, IRDBERepository<Exercise> exercisesRepo, IRDBERepository<WorkoutExercise> weRepo, UserManager<ApplicationUser> userManager)
        {
            this.workoutsRepo = workoutsRepo;
            this.exercisesRepo = exercisesRepo;
            this.weRepo = weRepo;
            this.userManager = userManager;
        }

        // GET: Workouts
        public async Task<IActionResult> Index()
        {
            using (workoutsRepo)
            using (userManager)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                var workouts = workoutsRepo.All();
                return View(await workouts.Where(w => w.ApplicationUserId == user.Id).ToListAsync());
            }
        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (workoutsRepo)
            using (userManager)
            { 
                var workout = workoutsRepo.GetById(id);

                var user = await userManager.GetUserAsync(HttpContext.User);

                if (workout == null || workout.ApplicationUserId != user.Id)
                {
                    return NotFound();
                }

                return View(workout);
            }
        }

        // GET: Workouts/Create
        public async Task<IActionResult> Create()
        {
            using (exercisesRepo)
            using (userManager)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                ViewBag.AllExerciseNames = exercisesRepo.All().Where(e => e.ApplicationUserId == user.Id).Select(e => new SelectListItem() { Text = e.ExerciseName, Value = e.ExerciseId.ToString() }).ToList();
            }

            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorkoutViewModel vm)
        {             
            if (ModelState.IsValid)
            {
                using (exercisesRepo)
                using (workoutsRepo)
                using (weRepo)
                using (userManager)
                {
                    var user = await userManager.GetUserAsync(HttpContext.User);
                    var exercises = exercisesRepo.All().Where(e => e.ApplicationUserId == user.Id).ToList();
                    var selectedExercises = vm.Exercises.Select(e => Int32.Parse(e));
                    var filteredExcercises = exercises.Where(e => selectedExercises.Contains(e.ExerciseId));

                    Workout workout = new Workout()
                    {
                        WorkoutName = vm.WorkoutName,
                        WorkoutDetails = vm.WorkoutDetails,
                        DateCreated = DateTime.Now,
                        ApplicationUserId = user.Id
                    };
                    workoutsRepo.Add(workout);

                    foreach (var e in filteredExcercises)
                    {
                        weRepo.Add(new WorkoutExercise() { Exercise = e, Workout = workout });
                    }


                    workoutsRepo.SaveChanges();
                    weRepo.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(vm);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (workoutsRepo)
            using (exercisesRepo)
            using (userManager)
            {

                var workout = workoutsRepo
                    .All()
                    .Where(w => w.WorkoutId == id)
                    .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                    .FirstOrDefault();

                var exercises = workout.WorkoutExercises.Select(we => we.Exercise).ToList();
                var exercisesId = exercises.Select(e => e.ExerciseId).ToList();
                
                if (workout == null)
                {
                    return NotFound();
                }

                var user = await userManager.GetUserAsync(HttpContext.User);

                var allUserExercises = exercisesRepo.All().Where(e => e.ApplicationUserId == user.Id).Select(e => new SelectListItem() { Text = e.ExerciseName, Value = e.ExerciseId.ToString() }).ToList();

                foreach(var e in allUserExercises)
                {
                    if(exercisesId.Contains(Int32.Parse(e.Value)))
                    {
                        e.Selected = true;
                    }
                }

                ViewBag.AllExercises = allUserExercises;

                EditWorkoutViewModel vm = new EditWorkoutViewModel()
                {
                    WorkoutId = workout.WorkoutId,
                    WorkoutName = workout.WorkoutName,
                    WorkoutDetails = workout.WorkoutDetails,
                    Exercises = exercises.Select(e => e.ExerciseId.ToString()).ToList()
                };

                return View(vm);
            }
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditWorkoutViewModel vm)
        {
            if (id != vm.WorkoutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.GetUserAsync(HttpContext.User);
                    var exercises = exercisesRepo.All().Where(e => e.ApplicationUserId == user.Id);
                    Workout workout = workoutsRepo.GetById(id);
                    var selectedExercises = vm.Exercises.Select(e => Int32.Parse(e));
                    workout.WorkoutName = vm.WorkoutName;
                    workout.WorkoutDetails = vm.WorkoutDetails;
                    var filteredExercises = exercises.Where(e => selectedExercises.Contains(e.ExerciseId)).ToList();
                    var filteredExercisesIds = filteredExercises.Select(fe => fe.ExerciseId).ToList();
                    workoutsRepo.Update(workout);

                    //get what is in db
                    var oldExercises = weRepo.All().Where(we => we.WorkoutId == id).ToList();
                    var oldExercisesIds = oldExercises.Select(ce => ce.ExerciseId).ToList();

                    var oldWE = new WorkoutExercise();

                    //remove not selected
                    foreach (var eId in oldExercisesIds)
                    {
                        if(!filteredExercisesIds.Contains(eId))
                        {
                            oldWE = oldExercises.Where(oe => oe.ExerciseId == eId).FirstOrDefault();

                            weRepo.Delete(oldWE);
                        }

                    }

                    //add selected not in db
                    foreach (var e in filteredExercises)
                    {
                        if(!oldExercisesIds.Contains(e.ExerciseId))
                        { 
                            weRepo.Add(new WorkoutExercise() { Exercise = e, Workout = workout });
                        }
                    }


                    workoutsRepo.SaveChanges();
                    weRepo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = workoutsRepo.GetById(id); ;
                    if (exists == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    workoutsRepo.Dispose();
                    exercisesRepo.Dispose();
                    weRepo.Dispose();
                    userManager.Dispose();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Workouts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (workoutsRepo)
            {
                var workout = workoutsRepo.GetById(id);

                if (workout == null)
                {
                    return NotFound();
                }


                return View(workout);
            }
            
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (workoutsRepo)
            {
                var workout = workoutsRepo.GetById(id);
                workoutsRepo.Delete(workout);
                workoutsRepo.SaveChanges();
                return RedirectToAction(nameof(Index));
            } 
        }
    }
}
