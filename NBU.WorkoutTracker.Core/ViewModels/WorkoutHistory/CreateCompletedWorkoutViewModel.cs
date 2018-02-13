using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class CreateCompletedWorkoutViewModel
    {
        public int WorkoutId { get; set; }

        public string WorkoutName { get; set; }

        public string Comments { get; set; }

        public IList<DetailedExerciseViewModel> DetailedExercises { get; set; }
    }
}
