using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElearningMVC.Migrations
{
    /// <inheritdoc />
    public partial class mcqs3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Correct",
                table: "Mcqss",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correct",
                table: "Mcqss");
        }
    }
}
