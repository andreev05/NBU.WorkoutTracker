using System;
using System.Collections.Generic;
using System.Text;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class DetailedExerciseViewModel
    {
        public int ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        public int? TargetReps { get; set; }

        public int? TargetSets { get; set; }

        public float? TargetWeight { get; set; }

        public int? TargetMins { get; set; }

        public int? Sets { get; set; }

        public int? Reps { get; set; }

        public float? Weight { get; set; }

        public int? Mins { get; set; }

        public string Comments { get; set; }
    }
}
