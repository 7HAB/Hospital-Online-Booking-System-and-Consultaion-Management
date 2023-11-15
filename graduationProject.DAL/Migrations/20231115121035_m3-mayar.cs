using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graduationProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m3mayar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitCount_WeekSchedules_WeekScheduleId",
                table: "VisitCount");

            migrationBuilder.DropIndex(
                name: "IX_VisitCount_WeekScheduleId",
                table: "VisitCount");

            migrationBuilder.AlterColumn<int>(
                name: "WeekScheduleId",
                table: "VisitCount",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VisitCountWeekSchedule",
                columns: table => new
                {
                    VisitCountId = table.Column<int>(type: "int", nullable: false),
                    WeekScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitCountWeekSchedule", x => new { x.VisitCountId, x.WeekScheduleId });
                    table.ForeignKey(
                        name: "FK_VisitCountWeekSchedule_VisitCount_VisitCountId",
                        column: x => x.VisitCountId,
                        principalTable: "VisitCount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitCountWeekSchedule_WeekSchedules_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitCountWeekSchedule_WeekScheduleId",
                table: "VisitCountWeekSchedule",
                column: "WeekScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitCountWeekSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "WeekScheduleId",
                table: "VisitCount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_VisitCount_WeekScheduleId",
                table: "VisitCount",
                column: "WeekScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitCount_WeekSchedules_WeekScheduleId",
                table: "VisitCount",
                column: "WeekScheduleId",
                principalTable: "WeekSchedules",
                principalColumn: "Id");
        }
    }
}
