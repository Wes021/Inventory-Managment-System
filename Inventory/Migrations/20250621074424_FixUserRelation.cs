using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Migrations
{
    /// <inheritdoc />
    public partial class FixUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stock_transactions_AspNetUsers_UsersId",
                table: "stock_transactions");

            migrationBuilder.DropIndex(
                name: "IX_stock_transactions_UsersId",
                table: "stock_transactions");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "stock_transactions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "stock_transactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_stock_transactions_UserId",
                table: "stock_transactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "stock_transactions_fk_user",
                table: "stock_transactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "stock_transactions_fk_user",
                table: "stock_transactions");

            migrationBuilder.DropIndex(
                name: "IX_stock_transactions_UserId",
                table: "stock_transactions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "stock_transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "stock_transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_stock_transactions_UsersId",
                table: "stock_transactions",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_stock_transactions_AspNetUsers_UsersId",
                table: "stock_transactions",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
