using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class EditWorkoutViewModel
    {
        public int WorkoutId { get; set; }

        public string WorkoutName { get; set; }

        public string WorkoutDetails { get; set; }

        public List<string> Exercises { get; set; }
    }
}
