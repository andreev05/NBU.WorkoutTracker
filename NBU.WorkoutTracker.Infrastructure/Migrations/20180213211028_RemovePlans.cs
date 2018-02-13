using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NBU.WorkoutTracker.Infrastructure.Migrations
{
    public partial class RemovePlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWorkouts_Plans_PlanId",
                table: "CompletedWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Plans_PlanId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_PlanId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_CompletedWorkouts_PlanId",
                table: "CompletedWorkouts");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "CompletedWorkouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "CompletedWorkouts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PlanName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_Plans_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_PlanId",
                table: "Workouts",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWorkouts_PlanId",
                table: "CompletedWorkouts",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ApplicationUserId",
                table: "Plans",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWorkouts_Plans_PlanId",
                table: "CompletedWorkouts",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Plans_PlanId",
                table: "Workouts",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
