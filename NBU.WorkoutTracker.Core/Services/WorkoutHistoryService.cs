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
        private readonly IRDBERepository<Exercise> excercisesRepo;

        public WorkoutHistoryService(
            IRDBERepository<CompletedWorkout> completedWorkoutsRepo, 
            IRDBERepository<CompletedExercise> completedExcercisesRepo,
            IRDBERepository<Exercise> excercisesRepo)
        {
            this.completedWorkoutsRepo = completedWorkoutsRepo;
            this.completedExcercisesRepo = completedExcercisesRepo;
        }


        public IEnumerable<CompletedWorkout> GetUserWorkoutHistory(string userId)
        {
            using (completedWorkoutsRepo)
            {                
                var workouts = completedWorkoutsRepo.All()
                    .Include(cw => cw.Workout)
                    .Include(cw => cw.CompletedExercises)
                    .ThenInclude(ce => ce.Exercise);
                return workouts.ToList();
            }
        }

        public void AddCompletedWorkout(string userId, CreateCompletedWorkoutViewModel vm)
        {
            var completedWorkout = new CompletedWorkout()
            {
                ApplicationUserId = userId,
                Comments = vm.Comments,
                DateCreated = DateTime.Now,
                WorkoutId = vm.WorkoutId,
                CompletedExercises = vm.DetailedExercises.Select(de => new CompletedExercise()
                {
                    ApplicationUserId = userId,
                    DateCreated = DateTime.Now,
                    Comments = de.Comments,
                    CompletedExerciseId = de.ExerciseId,
                    CompletedWorkoutId = vm.WorkoutId,
                    Mins = de.Mins,
                    Weight = de.Weight,
                    Sets = de.Sets,
                    Reps = de.Reps
                }).ToList()
            };

            using (completedWorkoutsRepo)
            {
                completedWorkoutsRepo.Add(completedWorkout);
                completedWorkoutsRepo.SaveChanges();
            }
        }


        public IEnumerable<DetailedExerciseViewModel> GetWorkoutExercises(string userId, int workoutId)
        {

            using (excercisesRepo)
            {
                var exercises = excercisesRepo.All().Where(e => e.ApplicationUserId == userId && e.WorkoutId == workoutId).ToList();

                return exercises.Select(e => new DetailedExerciseViewModel()
                {
                    ExerciseId = e.ExerciseId,
                    TargetMins = e.TargetMins,
                    TargetSets = e.TargetSets,
                    TargetReps = e.TargetReps,
                    TargetWeight = e.TargetWeight,
                    ExerciseName = e.ExerciseName
                });
            }
        }


        public bool CheckId(string userId, int workoutId)
        {
            throw new NotImplementedException();
        }


        public void AddExercisesToCompletedWorkout(string userId, int completedWorkoutId)
        {
            throw new NotImplementedException();
        }

        public void RemoveExerciseFromCompletedWorkout(string userId, int workoutId, int excerciseId)
        {
            throw new NotImplementedException();
        }
    }
}
