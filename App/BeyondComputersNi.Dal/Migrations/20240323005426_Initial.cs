using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyondComputersNi.Dal.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Computers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Motherboard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CPUCooler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Memory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Storage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                GPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PSU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Case = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Computers", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Computers");
    }
}
