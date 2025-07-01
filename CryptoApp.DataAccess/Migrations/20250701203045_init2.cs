using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinTransactions_TelegramUsers_UserId",
                table: "CoinTransactions");

            migrationBuilder.DropIndex(
                name: "IX_CoinTransactions_UserId",
                table: "CoinTransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CoinTransactions");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_TelegramUserId",
                table: "CoinTransactions",
                column: "TelegramUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinTransactions_TelegramUsers_TelegramUserId",
                table: "CoinTransactions",
                column: "TelegramUserId",
                principalTable: "TelegramUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinTransactions_TelegramUsers_TelegramUserId",
                table: "CoinTransactions");

            migrationBuilder.DropIndex(
                name: "IX_CoinTransactions_TelegramUserId",
                table: "CoinTransactions");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CoinTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransactions_UserId",
                table: "CoinTransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinTransactions_TelegramUsers_UserId",
                table: "CoinTransactions",
                column: "UserId",
                principalTable: "TelegramUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
