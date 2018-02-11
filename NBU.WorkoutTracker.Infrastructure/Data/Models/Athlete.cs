using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{
    /// <summary>
    /// Mapping between ApplicationUsers and Plans
    /// </summary>
    public class Athlete
    {
        public ApplicationUser AppUser { get; set; }

        public ICollection<Plan> Plans { get; set; }
    }
}
