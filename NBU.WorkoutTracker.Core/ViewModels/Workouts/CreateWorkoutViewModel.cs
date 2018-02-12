using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class CreateWorkoutViewModel
    {
        public string WorkoutName { get; set; }

        public string WorkoutDetails { get; set; }

        public List<SelectListItem> Exercises { get; set; }
    }
}
