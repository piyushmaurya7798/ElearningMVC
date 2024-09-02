using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElearningMVC.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isActive",
                table: "User_account",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "isActive",
                table: "User_account",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
