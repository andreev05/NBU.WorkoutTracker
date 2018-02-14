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


        public List<DetailedWorkoutViewModel> GetUserWorkoutHistory(string userId)
        {             
            var workouts = completedWorkoutsRepo.All()
                .Where(cw => cw.ApplicationUserId == userId)
                .Include(cw => cw.Workout)
                .Include(cw => cw.CompletedExercises)
                .ThenInclude(ce => ce.Exercise);

            List<DetailedWorkoutViewModel> detailedWorkouts
            = workouts.Select(w => new DetailedWorkoutViewModel()
            {
                WorkoutId = w.WorkoutId,
                WorkoutName = w.Workout.WorkoutName,
                DateCreated = w.DateCreated,
                Comments = w.Comments,
                DetailedExercises = w.CompletedExercises.Select(ce => new DetailedExerciseViewModel()
                {
                    ExerciseId = ce.Exercise.ExerciseId,
                    ExerciseName = ce.Exercise.ExerciseName,
                    TargetMins = ce.Exercise.TargetMins,
                    TargetSets = ce.Exercise.TargetSets,
                    TargetReps = ce.Exercise.TargetReps,
                    TargetWeight = ce.Exercise.TargetWeight,
                    Sets = ce.Sets,
                    Reps = ce.Reps,
                    Weight = ce.Weight,
                    Mins = ce.Mins,
                    Comments = ce.Comments
                }).ToList()
            }).ToList();

            return detailedWorkouts;
   
        }

        public void AddCompletedWorkout(string userId, CreateCompletedWorkoutViewModel vm)
        {
            var completedWorkout = new CompletedWorkout()
            {
                ApplicationUserId = userId,
                Comments = vm.Comments,
                DateCreated = DateTime.Now,
                WorkoutId = vm.WorkoutId
            };
            
            completedWorkoutsRepo.Add(completedWorkout);

            completedExcercisesRepo.AddRange(vm.DetailedExercises.Select(de => new CompletedExercise()
            {
                ApplicationUserId = userId,
                DateCreated = DateTime.Now,
                Comments = de.Comments,
                ExerciseId = de.ExerciseId,
                CompletedWorkout = completedWorkout,
                Mins = de.Mins,
                Weight = de.Weight,
                Sets = de.Sets,
                Reps = de.Reps
            }).ToList());

            completedWorkoutsRepo.SaveChanges();
            completedExcercisesRepo.SaveChanges();
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
            var workout = workoutsRepo.GetById(workoutId);
            return workout.WorkoutName;
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
