using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graduationProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m2mayar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DayOfWeek",
                table: "WeekSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LimitOfPatients",
                table: "VisitCount",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitOfPatients",
                table: "VisitCount");

            migrationBuilder.AlterColumn<string>(
                name: "DayOfWeek",
                table: "WeekSchedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
