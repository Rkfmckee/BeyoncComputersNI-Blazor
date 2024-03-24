using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyondComputersNi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ComputerUserNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Users_UserId",
                table: "Computers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Computers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Users_UserId",
                table: "Computers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Users_UserId",
                table: "Computers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Computers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Users_UserId",
                table: "Computers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
