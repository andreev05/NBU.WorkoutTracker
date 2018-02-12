using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NBU.WorkoutTracker.Core.Contracts;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.Services
{
    public class WorkoutHistoryService : IWorkoutHistory
    {
        private readonly IRDBERepository<CompletedWorkout> completedWorkoutsRepo;
        private readonly IRDBERepository<CompletedExercise> completedExcercisesRepo;

        public WorkoutHistoryService(IRDBERepository<CompletedWorkout> completedWorkoutsRepo, IRDBERepository<CompletedExercise> completedExcercisesRepo)
        {
            this.completedWorkoutsRepo = completedWorkoutsRepo;
            this.completedExcercisesRepo = completedExcercisesRepo;
        }


        public IEnumerable<CompletedWorkout> GetUserWorkoutHistory(string userId)
        {
            using (completedWorkoutsRepo)
            {                
                var workouts = completedWorkoutsRepo.All()
                    .Include(cw => cw.CompletedExercises)
                    .ThenInclude(ce => ce.Exercise);
                return workouts.ToList();
            }
        }

        public void AddCompletedWorkout(string userId)
        {
            throw new NotImplementedException();
        }

        public void AddExercisesToCompletedWorkout(string userId, int workoutId)
        {
            throw new NotImplementedException();
        }

        public void RemoveExerciseFromCompletedWorkout(string userId, int workoutId, int excerciseId)
        {
            throw new NotImplementedException();
        }
    }
}
