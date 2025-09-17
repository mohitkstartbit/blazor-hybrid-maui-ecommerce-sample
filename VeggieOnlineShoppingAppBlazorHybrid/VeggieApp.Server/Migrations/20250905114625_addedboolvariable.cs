using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeggieApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class addedboolvariable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAddedToCart",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAddedToCart",
                table: "Products");
        }
    }
}
