using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.ViewModels.WorkoutHistory
{
    public class ExerciseHistoryViewModel
    {
        public ICollection<CompletedExercise> CompletedExercises { get; set; }
    }
}
