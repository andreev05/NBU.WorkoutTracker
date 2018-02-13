using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NBU.WorkoutTracker.Infrastructure.Migrations
{
    public partial class ManyChangesToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedExercises_CompletedWorkouts_CompletedWorkoutId",
                table: "CompletedExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedExercises_Exercises_ExerciseId",
                table: "CompletedExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedExercises_Workouts_WorkoutId",
                table: "CompletedExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_CompletedExercises_WorkoutId",
                table: "CompletedExercises");

            migrationBuilder.DropColumn(
                name: "Cheat",
                table: "CompletedExercises");

            migrationBuilder.DropColumn(
                name: "TargetsMet",
                table: "CompletedExercises");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "CompletedExercises");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Plans",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "Exercises",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CompletedWorkouts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "CompletedWorkouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "CompletedExercises",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompletedWorkoutId",
                table: "CompletedExercises",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CompletedExercises",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutExercise",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercise", x => new { x.WorkoutId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ApplicationUserId",
                table: "Workouts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ApplicationUserId",
                table: "Plans",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ApplicationUserId",
                table: "Exercises",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWorkouts_ApplicationUserId",
                table: "CompletedWorkouts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWorkouts_WorkoutId",
                table: "CompletedWorkouts",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedExercises_ApplicationUserId",
                table: "CompletedExercises",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_ExerciseId",
                table: "WorkoutExercise",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedExercises_AspNetUsers_ApplicationUserId",
                table: "CompletedExercises",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedExercises_CompletedWorkouts_CompletedWorkoutId",
                table: "CompletedExercises",
                column: "CompletedWorkoutId",
                principalTable: "CompletedWorkouts",
                principalColumn: "CompletedWorkoutId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedExercises_Exercises_ExerciseId",
                table: "CompletedExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWorkouts_AspNetUsers_ApplicationUserId",
                table: "CompletedWorkouts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWorkouts_Workouts_WorkoutId",
                table: "CompletedWorkouts",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_AspNetUsers_ApplicationUserId",
                table: "Exercises",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserId",
                table: "Plans",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedExercises_AspNetUsers_ApplicationUserId",
                table: "CompletedExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedExercises_CompletedWorkouts_CompletedWorkoutId",
                table: "CompletedExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedExercises_Exercises_ExerciseId",
                table: "CompletedExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWorkouts_AspNetUsers_ApplicationUserId",
                table: "CompletedWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWorkouts_Workouts_WorkoutId",
                table: "CompletedWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_AspNetUsers_ApplicationUserId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "WorkoutExercise");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ApplicationUserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Plans_ApplicationUserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ApplicationUserId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_CompletedWorkouts_ApplicationUserId",
                table: "CompletedWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_CompletedWorkouts_WorkoutId",
                table: "CompletedWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_CompletedExercises_ApplicationUserId",
                table: "CompletedExercises");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CompletedWorkouts");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "CompletedWorkouts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CompletedExercises");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "Exercises",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "CompletedExercises",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CompletedWorkoutId",
                table: "CompletedExercises",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Cheat",
                table: "CompletedExercises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetsMet",
                table: "CompletedExercises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "CompletedExercises",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedExercises_WorkoutId",
                table: "CompletedExercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedExercises_CompletedWorkouts_CompletedWorkoutId",
                table: "CompletedExercises",
                column: "CompletedWorkoutId",
                principalTable: "CompletedWorkouts",
                principalColumn: "CompletedWorkoutId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedExercises_Exercises_ExerciseId",
                table: "CompletedExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedExercises_Workouts_WorkoutId",
                table: "CompletedExercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
