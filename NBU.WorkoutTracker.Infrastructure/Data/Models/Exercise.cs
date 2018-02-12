using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{
    /// <summary>
    /// The name and the target details about each exercise.
    /// </summary>
    public class Exercise
    {
        public int ExerciseId { get; set; }

        public DateTime DateCreated { get; set; }

        public string ExerciseName { get; set; }

        public int? TargetReps { get; set; }

        public int? TargetSets { get; set; }

        public float TargetWeight { get; set; }

        public int TargetMins { get; set; }

        public string ApplicationUserId { get; set; }

        public ICollection<CompletedExercise> CompletedExercises { get; set; }

        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
