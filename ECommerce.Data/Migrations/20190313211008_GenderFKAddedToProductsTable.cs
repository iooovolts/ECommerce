using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Data.Migrations
{
    public partial class GenderFKAddedToProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_GenderCategories_GenderCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_GenderCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "GenderCategoryId",
                table: "ProductCategories");

            migrationBuilder.AddColumn<int>(
                name: "GenderCategoryId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenderCategoryId",
                table: "Products",
                column: "GenderCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_GenderCategories_GenderCategoryId",
                table: "Products",
                column: "GenderCategoryId",
                principalTable: "GenderCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_GenderCategories_GenderCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_GenderCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GenderCategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "GenderCategoryId",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_GenderCategoryId",
                table: "ProductCategories",
                column: "GenderCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_GenderCategories_GenderCategoryId",
                table: "ProductCategories",
                column: "GenderCategoryId",
                principalTable: "GenderCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
