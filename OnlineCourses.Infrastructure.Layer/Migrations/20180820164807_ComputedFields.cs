using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourses.Infrastructure.Layer.Migrations
{
    public partial class ComputedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageAge",
                table: "Courses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumAge",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumAge",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSignatures",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageAge",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MaximumAge",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MinimumAge",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TotalSignatures",
                table: "Courses");
        }
    }
}
