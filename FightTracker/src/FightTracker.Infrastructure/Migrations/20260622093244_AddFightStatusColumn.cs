using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FightTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFightStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Fights",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fights");
        }
    }
}
