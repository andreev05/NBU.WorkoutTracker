using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NBU.WorkoutTracker.Infrastructure.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PlanName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "CompletedWorkouts",
                columns: table => new
                {
                    CompletedWorkoutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedWorkouts", x => x.CompletedWorkoutId);
                    table.ForeignKey(
                        name: "FK_CompletedWorkouts_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PlanId = table.Column<int>(nullable: true),
                    WorkoutDetails = table.Column<string>(nullable: true),
                    WorkoutName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.WorkoutId);
                    table.ForeignKey(
                        name: "FK_Workouts_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ExerciseName = table.Column<string>(nullable: true),
                    TargetMins = table.Column<int>(nullable: false),
                    TargetReps = table.Column<int>(nullable: true),
                    TargetSets = table.Column<int>(nullable: true),
                    TargetWeight = table.Column<float>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_Exercises_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompletedExercises",
                columns: table => new
                {
                    CompletedExerciseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cheat = table.Column<bool>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    CompletedWorkoutId = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: true),
                    Mins = table.Column<int>(nullable: false),
                    Reps = table.Column<int>(nullable: true),
                    Sets = table.Column<int>(nullable: true),
                    TargetsMet = table.Column<bool>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedExercises", x => x.CompletedExerciseId);
                    table.ForeignKey(
                        name: "FK_CompletedExercises_CompletedWorkouts_CompletedWorkoutId",
                        column: x => x.CompletedWorkoutId,
                        principalTable: "CompletedWorkouts",
                        principalColumn: "CompletedWorkoutId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompletedExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompletedExercises_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedExercises_CompletedWorkoutId",
                table: "CompletedExercises",
                column: "CompletedWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedExercises_ExerciseId",
                table: "CompletedExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedExercises_WorkoutId",
                table: "CompletedExercises",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWorkouts_PlanId",
                table: "CompletedWorkouts",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_PlanId",
                table: "Workouts",
                column: "PlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedExercises");

            migrationBuilder.DropTable(
                name: "CompletedWorkouts");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
