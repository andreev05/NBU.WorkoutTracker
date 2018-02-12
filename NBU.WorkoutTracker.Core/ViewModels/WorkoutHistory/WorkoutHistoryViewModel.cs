using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class WorkoutHistoryViewModel
    {
        public IEnumerable<CompletedWorkout> CompletedWorkouts { get; set; }
    }
}
