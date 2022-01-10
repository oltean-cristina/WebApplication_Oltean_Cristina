using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication_Oltean_Cristina.Migrations
{
    public partial class ExtendedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryName = table.Column<string>(maxLength: 50, nullable: false),
                    Adress = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryFood",
                columns: table => new
                {
                    DeliveryID = table.Column<int>(nullable: false),
                    FoodID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryFood", x => new { x.FoodID, x.DeliveryID });
                    table.ForeignKey(
                        name: "FK_DeliveryFood_Delivery_DeliveryID",
                        column: x => x.DeliveryID,
                        principalTable: "Delivery",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryFood_Food_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Food",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(nullable: false),
                    FoodID = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Food_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Food",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryFood_DeliveryID",
                table: "DeliveryFood",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_FoodID",
                table: "Order",
                column: "FoodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryFood");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Food");
        }
    }
}
