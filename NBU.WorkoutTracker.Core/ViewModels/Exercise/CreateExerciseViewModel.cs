using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class CreateExerciseViewModel
    {
        public List<SelectListItem> Workouts { get; set; }

        public Exercise Exercise { get; set; }
    }
}
