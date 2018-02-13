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

        public DateTime DateCreated { get; set; }

        public string Comments { get; set; }

        public List<DetailedExerciseViewModel> DetailedExercises { get; set; }
    }
}
