using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class DetailedWorkoutViewModel
    {
        public string WorkoutName { get; set; }

        public int WorkoutId { get; set; }

        public CompletedWorkout CompletedWorkout { get; set; }

        public List<DetailedExerciseViewModel> DetailedExercises { get; set; }
    }
}
