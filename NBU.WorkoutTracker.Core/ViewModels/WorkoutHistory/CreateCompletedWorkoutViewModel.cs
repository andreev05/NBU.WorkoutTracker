using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class CreateCompletedWorkoutViewModel
    {
        public int WorkoutId { get; set; }

        public string Comments { get; set; }

        public IEnumerable<DetailedExerciseViewModel> DetailedExercises { get; set; }
    }
}
