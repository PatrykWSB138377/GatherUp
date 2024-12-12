using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatherUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventToInvitations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventInvitationBase_EventId",
                table: "EventInvitationBase",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventInvitationBase_Event_EventId",
                table: "EventInvitationBase",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInvitationBase_Event_EventId",
                table: "EventInvitationBase");

            migrationBuilder.DropIndex(
                name: "IX_EventInvitationBase_EventId",
                table: "EventInvitationBase");
        }
    }
}
