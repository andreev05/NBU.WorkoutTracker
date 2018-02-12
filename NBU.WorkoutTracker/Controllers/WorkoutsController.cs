using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Contexts;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IRDBERepository<Workout> workoutsRepo;
        private readonly IRDBERepository<Exercise> exercisesRepo;

        public WorkoutsController(IRDBERepository<Workout> workoutsRepo, IRDBERepository<Exercise> exercisesRepo)
        {
            this.workoutsRepo = workoutsRepo;
            this.exercisesRepo = exercisesRepo;
        }

        // GET: Workouts
        public async Task<IActionResult> Index()
        {
            using (workoutsRepo)
            { 
                var workouts = workoutsRepo.All();
                return View(await workouts.ToListAsync());
            }
        }

        // GET: Workouts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = workoutsRepo.GetById(id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            using (exercisesRepo)
            {
                ViewBag.AllExerciseNames = exercisesRepo.All().Select(e => new SelectListItem() { Text = e.ExerciseName }).ToList();
            }

            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateWorkoutViewModel vm)
        {             
            if (ModelState.IsValid)
            {
                using (exercisesRepo)
                using (workoutsRepo)
                {
                    var exercises = exercisesRepo.All().ToList();
                    var selectedExercisesNames = vm.Exercises.Where(e => e.Selected == true).Select(e => e.Text).ToList();

                    Workout workout = new Workout()
                    {
                        WorkoutName = vm.WorkoutName,
                        WorkoutDetails = vm.WorkoutDetails,
                        DateCreated = DateTime.Now,
                        Exercises = exercises.Where(e => selectedExercisesNames.Contains(e.ExerciseName)).ToList()
                    };

                    workoutsRepo.Add(workout);
                    workoutsRepo.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(vm);
        }

        // GET: Workouts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (workoutsRepo)
            using (exercisesRepo)
            {

                var workout = workoutsRepo.GetById(id);
                
                if (workout == null)
                {
                    return NotFound();
                }

                ViewBag.AllExerciseNames = exercisesRepo.All().Select(e => new SelectListItem() { Text = e.ExerciseName }).ToList();
                EditWorkoutViewModel vm = new EditWorkoutViewModel()
                {
                    WorkoutId = workout.WorkoutId,
                    WorkoutName = workout.WorkoutName,
                    WorkoutDetails = workout.WorkoutDetails,
                    Exercises = workout.Exercises.Select(e => new SelectListItem()
                    {
                        Text = e.ExerciseName
                    }).ToList()
                };
                return View(vm);
            }
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditWorkoutViewModel vm)
        {
            if (id != vm.WorkoutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var exercises = exercisesRepo.All();
                    Workout workout = workoutsRepo.GetById(id);

                    var selectedExercises = vm.Exercises.Where(e => e.Selected == true).Select(e => e.Text);
                    workout.WorkoutName = vm.WorkoutName;
                    workout.WorkoutDetails = vm.WorkoutDetails;
                    workout.Exercises = exercises.Where(e => selectedExercises.Contains(e.ExerciseName)).ToList();

                    workoutsRepo.Update(workout);

                    
                    workoutsRepo.SaveChanges();
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
