using System;
using System.Collections.Generic;
using System.Text;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{
    /// <summary>
    /// Contains the completedexercises and comments about the actual workout.
    /// </summary>
    public class CompletedWorkout
    {
        public int CompletedWorkoutId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Comments { get; set; }

        public ICollection<CompletedExercise> CompletedExercises { get; set; }
    }
}
