using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.Contracts
{
    public interface IWorkoutHistory
    {
        //List<WorkoutHistoryViewModel> GetUserWorkoutHistory(string userId);
        IEnumerable<CompletedWorkout> GetUserWorkoutHistory(string userId);

        void AddCompletedWorkout(string userId);

        void AddExercisesToCompletedWorkout(string userId, int workoutId);

        void RemoveExerciseFromCompletedWorkout(string userId, int workoutId, int excerciseId);
    }
}
