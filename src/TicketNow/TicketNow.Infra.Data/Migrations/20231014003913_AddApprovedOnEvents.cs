using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketNow.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovedOnEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Event",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Event");
        }
    }
}
