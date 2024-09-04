using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94bee9a2-c0ab-4905-b4cb-f70a32044950");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d4c3670-2c80-44b8-b5c3-8153a186350e");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "comments",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "db1fc663-1493-4f9d-8a5b-7843d93233a8", null, "user", "USER" },
                    { "e9730324-5a9d-4bee-982c-fdef33bf4a36", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db1fc663-1493-4f9d-8a5b-7843d93233a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9730324-5a9d-4bee-982c-fdef33bf4a36");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "comments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94bee9a2-c0ab-4905-b4cb-f70a32044950", null, "user", "USER" },
                    { "9d4c3670-2c80-44b8-b5c3-8153a186350e", null, "admin", "ADMIN" }
                });
        }
    }
}
