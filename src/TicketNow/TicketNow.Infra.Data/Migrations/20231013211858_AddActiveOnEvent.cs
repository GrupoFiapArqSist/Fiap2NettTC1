using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketNow.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveOnEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_PrometerId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "PrometerId",
                table: "Event",
                newName: "PromoterId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_PrometerId",
                table: "Event",
                newName: "IX_Event_PromoterId");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Event",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_PromoterId",
                table: "Event",
                column: "PromoterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_PromoterId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "PromoterId",
                table: "Event",
                newName: "PrometerId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_PromoterId",
                table: "Event",
                newName: "IX_Event_PrometerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_PrometerId",
                table: "Event",
                column: "PrometerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
