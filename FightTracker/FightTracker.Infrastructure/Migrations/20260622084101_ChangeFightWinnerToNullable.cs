using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FightTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFightWinnerToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fights_Fighters_WinnerId",
                table: "Fights");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Fights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Fights_Fighters_WinnerId",
                table: "Fights",
                column: "WinnerId",
                principalTable: "Fighters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fights_Fighters_WinnerId",
                table: "Fights");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Fights",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fights_Fighters_WinnerId",
                table: "Fights",
                column: "WinnerId",
                principalTable: "Fighters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
