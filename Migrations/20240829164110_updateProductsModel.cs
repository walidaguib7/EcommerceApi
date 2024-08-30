using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updateProductsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28604124-2a1b-4bf2-b45b-82a4b1dca9f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f817e23-87b9-4d42-aee9-ecab0a260e2f");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "products",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15d7da1a-1b1d-4eda-add9-f805e5aefdc6", null, "admin", "ADMIN" },
                    { "3849a8d1-8d8c-49b9-b7fa-529482cfe274", null, "user", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_userId",
                table: "products",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_AspNetUsers_userId",
                table: "products",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_AspNetUsers_userId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_userId",
                table: "products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15d7da1a-1b1d-4eda-add9-f805e5aefdc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3849a8d1-8d8c-49b9-b7fa-529482cfe274");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28604124-2a1b-4bf2-b45b-82a4b1dca9f1", null, "user", "USER" },
                    { "8f817e23-87b9-4d42-aee9-ecab0a260e2f", null, "admin", "ADMIN" }
                });
        }
    }
}
