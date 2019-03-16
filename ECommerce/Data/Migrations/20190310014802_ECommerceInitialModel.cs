using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Data.Migrations
{
    public partial class ECommerceInitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderCategoryId",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GenderCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderCategories", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_GenderCategories_GenderCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropTable(
                name: "GenderCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_GenderCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GenderCategoryId",
                table: "ProductCategories");
        }
    }
}
