using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NBU.WorkoutTracker.Infrastructure.Data.Models;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Infrastructure.Data.Contexts
{
    public class WorkoutTrackerDbContext : IdentityDbContext<ApplicationUser>
    {

        /// <summary>
        /// Tables with items
        /// </summary>


        public DbSet<Plan> Plans { get; set; }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<CompletedWorkout> CompletedWorkouts { get; set; }

        public DbSet<CompletedExercise> CompletedExercises { get; set; }


        public WorkoutTrackerDbContext(DbContextOptions<WorkoutTrackerDbContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Database description - tables names, foreign keys etc.
        /// It can be substituted by Data anotacion attributes in models
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<WorkoutExercise>().HasKey(table => new {
                table.WorkoutId,
                table.ExerciseId
            });
        }
    }
}
