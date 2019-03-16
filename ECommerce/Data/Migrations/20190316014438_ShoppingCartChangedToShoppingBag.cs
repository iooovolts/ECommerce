using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Data.Migrations
{
    public partial class ShoppingCartChangedToShoppingBag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                newName: "ShoppingBags");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItems",
                newName: "ShoppingBagItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingBags",
                newName: "IX_ShoppingBags_UserId");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "ShoppingBagItems",
                newName: "ShoppingBagId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingBagItems",
                newName: "IX_ShoppingBagItems_ShoppingBagId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingBagItems",
                newName: "IX_ShoppingBagItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingBags",
                table: "ShoppingBags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingBagItems",
                table: "ShoppingBagItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBagItems_Products_ProductId",
                table: "ShoppingBagItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBagItems_ShoppingBags_ShoppingBagId",
                table: "ShoppingBagItems",
                column: "ShoppingBagId",
                principalTable: "ShoppingBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_AspNetUsers_UserId",
                table: "ShoppingBags",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBagItems_Products_ProductId",
                table: "ShoppingBagItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBagItems_ShoppingBags_ShoppingBagId",
                table: "ShoppingBagItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_AspNetUsers_UserId",
                table: "ShoppingBags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingBags",
                table: "ShoppingBags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingBagItems",
                table: "ShoppingBagItems");

            migrationBuilder.RenameTable(
                name: "ShoppingBags",
                newName: "ShoppingCarts");

            migrationBuilder.RenameTable(
                name: "ShoppingBagItems",
                newName: "ShoppingCartItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBags_UserId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_UserId");

            migrationBuilder.RenameColumn(
                name: "ShoppingBagId",
                table: "ShoppingCartItems",
                newName: "ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBagItems_ShoppingBagId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBagItems_ProductId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
