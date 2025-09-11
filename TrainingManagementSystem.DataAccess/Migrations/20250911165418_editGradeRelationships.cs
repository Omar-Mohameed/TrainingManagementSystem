using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManagementSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editGradeRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Grades",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Grades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GradeById",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_GradeById",
                table: "Grades",
                column: "GradeById");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId",
                table: "Grades",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_GradeById",
                table: "Grades",
                column: "GradeById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_UserId",
                table: "Grades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_GradeById",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_UserId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_GradeById",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "GradeById",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Grades");

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Grades",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
