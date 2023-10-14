using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketNow.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategoryIdFromEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Event");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
