using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElearningMVC.Migrations
{
    /// <inheritdoc />
    public partial class mcqs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mcqss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    option1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    option2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    option3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    option4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mcqss", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mcqss");
        }
    }
}
