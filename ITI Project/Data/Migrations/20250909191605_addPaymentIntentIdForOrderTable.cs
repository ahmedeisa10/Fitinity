using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class addPaymentIntentIdForOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");
        }
    }
}
