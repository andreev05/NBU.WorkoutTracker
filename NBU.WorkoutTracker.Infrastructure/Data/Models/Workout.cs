using System;
using System.Collections.Generic;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{

    /// <summary>
    /// Contains what exercises should be done during a given workout session.
    /// </summary>
    public class Workout
    {
        public int WorkoutId { get; set; }

        public DateTime DateCreated { get; set; }

        public string WorkoutName { get; set; }

        public string WorkoutDetails { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public ICollection<CompletedExercise> CompletedExercises { get; set; }
    }
}
