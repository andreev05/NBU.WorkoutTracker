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
        private readonly IRDBERepository<Workout> workoutsRepo;
        private readonly IRDBERepository<CompletedWorkout> completedWorkoutsRepo;
        private readonly IRDBERepository<CompletedExercise> completedExcercisesRepo;
        private readonly IRDBERepository<Exercise> excercisesRepo;

        public WorkoutHistoryService(
            IRDBERepository<Workout> workoutsRepo,
            IRDBERepository<CompletedWorkout> completedWorkoutsRepo, 
            IRDBERepository<CompletedExercise> completedExcercisesRepo,
            IRDBERepository<Exercise> excercisesRepo)
        {
            this.workoutsRepo = workoutsRepo;
            this.completedWorkoutsRepo = completedWorkoutsRepo;
            this.completedExcercisesRepo = completedExcercisesRepo;
            this.excercisesRepo = excercisesRepo;
        }


        public IEnumerable<CompletedWorkout> GetUserWorkoutHistory(string userId)
        {             
            var workouts = completedWorkoutsRepo.All()
                .Include(cw => cw.Workout)
                .Include(cw => cw.CompletedExercises)
                .ThenInclude(ce => ce.Exercise);
            return workouts.ToList();
   
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
            
                completedWorkoutsRepo.Add(completedWorkout);
                completedWorkoutsRepo.SaveChanges();
        }


        public IEnumerable<DetailedExerciseViewModel> GetWorkoutExercises(string userId, int workoutId)
        {
            var workout = workoutsRepo
                .All()
                .Where(w => w.WorkoutId == workoutId && w.ApplicationUserId == userId)
                .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
                .FirstOrDefault();

            return workout.WorkoutExercises.Select(we => new DetailedExerciseViewModel()
            {
                ExerciseId = we.Exercise.ExerciseId,
                TargetMins = we.Exercise.TargetMins,
                TargetSets = we.Exercise.TargetSets,
                TargetReps = we.Exercise.TargetReps,
                TargetWeight = we.Exercise.TargetWeight,
                ExerciseName = we.Exercise.ExerciseName
            });
        }


        public bool CheckUserId(string userId, int workoutId)
        {
            var workout = workoutsRepo.GetById(workoutId);
            bool isCorrectUser = false;

            if (userId == workout.ApplicationUserId)
            {
                isCorrectUser = true;
            }
            else
            {
                isCorrectUser = false;
            }

            return isCorrectUser;
        }

        public string GetWorkoutName(int workoutId)
        {
            return null;
        }


        public void AddExercisesToCompletedWorkout(string userId, int completedWorkoutId)
        {
            throw new NotImplementedException();
        }

        public void RemoveExerciseFromCompletedWorkout(string userId, int workoutId, int excerciseId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            workoutsRepo.Dispose();
            completedWorkoutsRepo.Dispose();
            completedExcercisesRepo.Dispose();
            excercisesRepo.Dispose();
        }
    }
}
