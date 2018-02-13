using System;
using System.Collections.Generic;
using System.Text;
using NBU.WorkoutTracker.Core.ViewModels;
using NBU.WorkoutTracker.Infrastructure.Data.Models;

namespace NBU.WorkoutTracker.Core.Contracts
{
    public interface IWorkoutHistory : IDisposable
    {
        IEnumerable<CompletedWorkout> GetUserWorkoutHistory(string userId);

        void AddCompletedWorkout(string userId, CreateCompletedWorkoutViewModel vm);

        void AddExercisesToCompletedWorkout(string userId, int completedWorkoutId);

        void RemoveExerciseFromCompletedWorkout(string userId, int workoutId, int excerciseId);

        IEnumerable<DetailedExerciseViewModel> GetWorkoutExercises(string userId, int workoutId);

        bool CheckUserId(string userId, int workoutId);
    }
}