using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSessionIdForOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatBot");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "ChatBot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BotReply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBot", x => x.Id);
                });
        }
    }
}
