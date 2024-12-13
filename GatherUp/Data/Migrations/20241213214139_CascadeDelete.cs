using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatherUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFollow_Event_EventId",
                table: "EventFollow");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFollow_Event_EventId",
                table: "EventFollow",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFollow_Event_EventId",
                table: "EventFollow");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFollow_Event_EventId",
                table: "EventFollow",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");
        }
    }
}
