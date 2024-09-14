using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updateReviewsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_comments_commentId",
                table: "reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a5f323a-a63e-4d6a-8a33-6f481121e56f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6335880-9b41-41e9-a88d-52a5606d9937");

            migrationBuilder.AlterColumn<int>(
                name: "rating",
                table: "reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "commentId",
                table: "reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e8fdf55-2bf0-40c7-b6af-380391f3d8ec", null, "admin", "ADMIN" },
                    { "a4b2d418-d6d5-4049-9552-decef93d9a61", null, "user", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_comments_commentId",
                table: "reviews",
                column: "commentId",
                principalTable: "comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_comments_commentId",
                table: "reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e8fdf55-2bf0-40c7-b6af-380391f3d8ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4b2d418-d6d5-4049-9552-decef93d9a61");

            migrationBuilder.AlterColumn<decimal>(
                name: "rating",
                table: "reviews",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "commentId",
                table: "reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a5f323a-a63e-4d6a-8a33-6f481121e56f", null, "user", "USER" },
                    { "e6335880-9b41-41e9-a88d-52a5606d9937", null, "admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_comments_commentId",
                table: "reviews",
                column: "commentId",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
