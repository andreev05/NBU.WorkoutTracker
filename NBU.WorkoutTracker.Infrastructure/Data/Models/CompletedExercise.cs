using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{
    /// <summary>
    /// The actual completed exercise and comments.
    /// </summary>
    public class CompletedExercise
    {
        public int CompletedExerciseId { get; set; }

        public DateTime DateCreated { get; set; }

        public int? Sets { get; set; }

        public int? Reps { get; set; }

        public float Weight { get; set; }

        public int Mins { get; set; }

        public bool Cheat { get; set; }


        /// <summary>
        /// The value should be set based the targets when the exercise was completed.
        /// </summary>
        public bool TargetsMet { get; set; }

        public string Comments { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public int CompletedWorkoutId { get; set; }

        public CompletedWorkout CompletedWorkout { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
