using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeggieApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class msix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_order_id",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_order_id",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "order_id",
                table: "OrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "order_id",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_order_id",
                table: "OrderItems",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_order_id",
                table: "OrderItems",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}
