using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyondComputersNi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddBuildNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildNumber",
                table: "Builds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildNumber",
                table: "Builds");
        }
    }
}
