using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingManagementSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditSessionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Sessions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Sessions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_InstructorId",
                table: "Sessions",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_InstructorId",
                table: "Sessions",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_InstructorId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_InstructorId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Sessions");
        }
    }
}
