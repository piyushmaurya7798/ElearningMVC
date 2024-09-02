using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElearningMVC.Migrations
{
    /// <inheritdoc />
    public partial class certificate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Certificates");

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Certificates");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Certificates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
