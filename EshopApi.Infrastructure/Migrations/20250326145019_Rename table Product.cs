using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EshopApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenametableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Price",
                table: "Products",
                newName: "IX_Products_Price");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Name",
                table: "Products",
                newName: "IX_Products_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Price",
                table: "Product",
                newName: "IX_Product_Price");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Name",
                table: "Product",
                newName: "IX_Product_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");
        }
    }
}
