using System;
using System.Collections.Generic;
using System.Text;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{
    public class WorkoutExercise
    {
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
