using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatherUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserIdOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_UserId",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Event",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_UserId",
                table: "Event",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_UserId",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Event",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_UserId",
                table: "Event",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
