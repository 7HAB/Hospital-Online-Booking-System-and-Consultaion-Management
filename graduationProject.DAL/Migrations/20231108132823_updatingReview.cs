using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graduationProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatingReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerformanceRate",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "patientVisitsWithDoctors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "patientVisitsWithDoctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "patientVisitsWithDoctors");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "patientVisitsWithDoctors");

            migrationBuilder.AddColumn<int>(
                name: "PerformanceRate",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
