using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatherUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBackToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Event",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserId",
                table: "Event",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_UserId",
                table: "Event",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_UserId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_UserId",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
