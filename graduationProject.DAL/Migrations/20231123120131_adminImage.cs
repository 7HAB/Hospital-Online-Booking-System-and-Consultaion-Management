using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graduationProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adminImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredFileName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "Admins");
        }
    }
}
