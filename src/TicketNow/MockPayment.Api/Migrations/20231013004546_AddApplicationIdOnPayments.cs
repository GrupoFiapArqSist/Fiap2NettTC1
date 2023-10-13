using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockPayment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationIdOnPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Payments",
                newName: "PaymentMethod");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Applications_ApplicationId",
                table: "Payments",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Applications_ApplicationId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Payments",
                newName: "PaymentType");
        }
    }
}
