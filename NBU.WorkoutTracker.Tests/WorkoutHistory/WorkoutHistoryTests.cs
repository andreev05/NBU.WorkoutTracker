using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Data.Models;
using System.Linq;
using NBU.WorkoutTracker.Core.Services;
using NBU.WorkoutTracker.Core.ViewModels;

namespace NBU.WorkoutTracker.Tests.WorkoutHistory
{
    [TestFixture]
    public class WorkoutHistoryTests
    {
        [Test]
        public void ShouldReturnWorkoutHistory()
        {
            //Arrange
            var fakeCompletedWorkouts = new List<CompletedWorkout>
            {
                new CompletedWorkout()
                {
                    ApplicationUserId = "testUser",
                    CompletedWorkoutId = 1,
                    DateCreated = DateTime.Now,
                    WorkoutId = 1,
                    CompletedExercises = new List<CompletedExercise>()
                    {
                        new CompletedExercise()
                        {
                            ApplicationUserId = "testUser",
                            DateCreated = DateTime.Now,
                            Mins = 12,
                            CompletedWorkoutId = 1,
                            ExerciseId = 1,
                            Exercise = new Exercise()
                            {
                                ExerciseName = "running",
                                ApplicationUserId = "testuser",
                                DateCreated = DateTime.Now,
                                TargetMins = 20
                            }
                        }
                    }
                }
            }.AsQueryable();
            var mockCompletedWorkoutsRepo = new Mock<IRDBERepository<CompletedWorkout>>();
            mockCompletedWorkoutsRepo.Setup(w => w.All()).Returns(fakeCompletedWorkouts);

            var mockCompletedExercisesRepo = new Mock<IRDBERepository<CompletedExercise>>();
            var mockExercisesRepo = new Mock<IRDBERepository<Exercise>>();

            var mockWorkoutsRepo = new Mock<IRDBERepository<Workout>>();

            //Act
            var mockWorkoutHistoryService = new WorkoutHistoryService(mockWorkoutsRepo.Object, mockCompletedWorkoutsRepo.Object, mockCompletedExercisesRepo.Object,  mockExercisesRepo.Object);
            var result = mockWorkoutHistoryService.GetUserWorkoutHistory("");

            //Assert
            Assert.AreEqual(result.ToList().Count, 1);
            Assert.AreEqual(result.FirstOrDefault().DetailedExercises.FirstOrDefault().ExerciseName, "running");
        }

        [Test]
        public void ShouldCallAddRepoMethods()
        {
            //Arrange

            var fakeCreateCompletedWorkoutViewModel = new CreateCompletedWorkoutViewModel
            {
                Comments = "workout comments",
                WorkoutId = 1,
                DetailedExercises = new List<DetailedExerciseViewModel>()
                {
                    new DetailedExerciseViewModel()
                    {
                        ExerciseId = 1,
                        ExerciseName = "testExercise",
                        Mins = 12,
                        Comments = "exercise comments"
                    }
                }
            };

            var mockCompletedWorkoutsRepo = new Mock<IRDBERepository<CompletedWorkout>>();
            var mockCompletedExercisesRepo = new Mock<IRDBERepository<CompletedExercise>>();
            var mockWorkoutsRepo = new Mock<IRDBERepository<Workout>>();
            var mockExercisesRepo = new Mock<IRDBERepository<Exercise>>();

            //Act
            var mockWorkoutHistoryService = new WorkoutHistoryService(mockWorkoutsRepo.Object, mockCompletedWorkoutsRepo.Object, mockCompletedExercisesRepo.Object, mockExercisesRepo.Object);
            mockWorkoutHistoryService.AddCompletedWorkout("", fakeCreateCompletedWorkoutViewModel);

            //Assert
            mockCompletedWorkoutsRepo.Verify(x => x.Add(It.IsAny<CompletedWorkout>()));
        }
    }
}
