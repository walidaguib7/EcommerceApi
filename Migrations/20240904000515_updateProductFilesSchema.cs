using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updateProductFilesSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a8a87f9-51f0-466c-87b0-e410c46bcadb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "887ac148-8a7b-4b7e-afdf-7165a336a1f9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94bee9a2-c0ab-4905-b4cb-f70a32044950", null, "user", "USER" },
                    { "9d4c3670-2c80-44b8-b5c3-8153a186350e", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94bee9a2-c0ab-4905-b4cb-f70a32044950");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d4c3670-2c80-44b8-b5c3-8153a186350e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a8a87f9-51f0-466c-87b0-e410c46bcadb", null, "admin", "ADMIN" },
                    { "887ac148-8a7b-4b7e-afdf-7165a336a1f9", null, "user", "USER" }
                });
        }
    }
}
