using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Contexts;
using NBU.WorkoutTracker.Infrastructure.Data.Models;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly WorkoutTrackerDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public ExercisesController(WorkoutTrackerDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            using (userManager)
            using (_context)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                return View(await _context.Exercises.Where(e => e.ApplicationUserId == user.Id).ToListAsync());
            }
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (_context)
            { 
                var exercise = await _context.Exercises
                    .SingleOrDefaultAsync(m => m.ExerciseId == id);
                if (exercise == null)
                {
                    return NotFound();
                }

                return View(exercise);
            }
        }

        // GET: Exercises/Create
        public async Task<IActionResult> Create()
        {
            using (_context)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                return View();
            }
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,ExerciseName,TargetReps,TargetSets,TargetWeight,TargetMins")] Exercise exercise)
        {
            
                
            if (ModelState.IsValid)
            {
                using (userManager)
                using (_context)
                {
                    var user = await userManager.GetUserAsync(HttpContext.User);
                    exercise.ApplicationUserId = user.Id;
                    exercise.ApplicationUser = user;
                    exercise.DateCreated = DateTime.Now;
                    _context.Add(exercise);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (_context)
            { 
                var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ExerciseId == id);
                if (exercise == null)
                {
                    return NotFound();
                }
                return View(exercise);
            }
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,ExerciseName,TargetReps,TargetSets,TargetWeight,TargetMins")] Exercise exercise)
        {
            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (userManager)
                    {
                        var user = await userManager.GetUserAsync(HttpContext.User);
                        if (exercise.ApplicationUserId != user.Id)
                        {
                            return NotFound();
                        }
                    }

                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId))
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
                    _context.Dispose();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .SingleOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (_context)
            { 
                var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ExerciseId == id);
                using (userManager)
                {
                    var user = await userManager.GetUserAsync(HttpContext.User);
                    if(exercise.ApplicationUserId != user.Id)
                    {
                        return NotFound();
                    }
                }

                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();    
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}
