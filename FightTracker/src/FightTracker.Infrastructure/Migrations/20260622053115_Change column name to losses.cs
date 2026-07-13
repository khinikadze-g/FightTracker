using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FightTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changecolumnnametolosses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Loses",
                table: "Fighters",
                newName: "Losses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Losses",
                table: "Fighters",
                newName: "Loses");
        }
    }
}
