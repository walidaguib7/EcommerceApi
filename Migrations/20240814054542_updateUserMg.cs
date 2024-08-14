using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updateUserMg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1cbdf5b-5eae-43bf-a340-2ea0df739bef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5d10344-ee33-4548-8308-c8b23b54c568");

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Order_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Order_Status = table.Column<string>(type: "longtext", nullable: false),
                    userId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdateAt = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    userId = table.Column<string>(type: "varchar(255)", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ordersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => new { x.ProductId, x.ordersId });
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_ordersId",
                        column: x => x.ordersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4cbd92f0-f921-47dc-a80b-31d5fac4614d", null, "user", "USER" },
                    { "a94e985d-c561-4ace-b557-27e96197d9e4", null, "admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_ordersId",
                table: "OrdersProducts",
                column: "ordersId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_orderId",
                table: "Payments",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_userId",
                table: "Payments",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersProducts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cbd92f0-f921-47dc-a80b-31d5fac4614d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a94e985d-c561-4ace-b557-27e96197d9e4");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f1cbdf5b-5eae-43bf-a340-2ea0df739bef", null, "admin", "ADMIN" },
                    { "f5d10344-ee33-4548-8308-c8b23b54c568", null, "user", "USER" }
                });
        }
    }
}
