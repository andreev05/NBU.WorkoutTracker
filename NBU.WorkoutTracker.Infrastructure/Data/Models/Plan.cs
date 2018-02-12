using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{
    /// <summary>
    /// Contains what workouts should be done. A user can be working on more than one plan.
    /// </summary>
    public class Plan
    {
        public int PlanId { get; set; }

        public DateTime DateCreated { get; set; }

        public string PlanName { get; set; }

        /// <summary>
        /// What workouts are planned as part of the plan.
        /// </summary>
        public ICollection<Workout> Workouts { get; set; }


        /// <summary>
        /// What workouts are completed.
        /// </summary>
        public ICollection<CompletedWorkout> CompletedWorkouts { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
