using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarWebApplication.Migrations
{
    public partial class InitialSmarterAsp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "DailyDeal",
                columns: table => new
                {
                    DailyDealID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyDealName = table.Column<string>(maxLength: 50, nullable: true),
                    DailyDealImage = table.Column<byte[]>(nullable: true),
                    DailyDealDescription = table.Column<string>(maxLength: 500, nullable: true),
                    DailyDealPrice = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    DailyDealQuantity = table.Column<int>(nullable: true),
                    DailyDealDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DailyDea__2E1B31B29C667241", x => x.DailyDealID);
                });

            migrationBuilder.CreateTable(
                name: "OrderType",
                columns: table => new
                {
                    TypeID = table.Column<bool>(nullable: false),
                    OrderType = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderTyp__516F0395FC748589", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    PeopleNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    ItemDescription = table.Column<string>(maxLength: 500, nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK__Item__Category__398D8EEE",
                        column: x => x.Category,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyDealOrder",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyDeal = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    OrderType = table.Column<bool>(nullable: false),
                    CurrentStatus = table.Column<bool>(nullable: true),
                    OrderMessage = table.Column<string>(maxLength: 500, nullable: true),
                    Table = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyDealOrder", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_DailyDealOrder_DailyDeal_DailyDeal",
                        column: x => x.DailyDeal,
                        principalTable: "DailyDeal",
                        principalColumn: "DailyDealID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyDealOrder_OrderType_OrderType",
                        column: x => x.OrderType,
                        principalTable: "OrderType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    OrderType = table.Column<bool>(nullable: false),
                    CurrentStatus = table.Column<bool>(nullable: true),
                    OrderMessage = table.Column<string>(maxLength: 500, nullable: true),
                    Table = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ItemOrde__C3905BAF9BDD4A3C", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__ItemOrder__Item__3E52440B",
                        column: x => x.Item,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ItemOrder__Order__3F466844",
                        column: x => x.OrderType,
                        principalTable: "OrderType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyDealOrder_DailyDeal",
                table: "DailyDealOrder",
                column: "DailyDeal");

            migrationBuilder.CreateIndex(
                name: "IX_DailyDealOrder_OrderType",
                table: "DailyDealOrder",
                column: "OrderType");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Category",
                table: "Item",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_Item",
                table: "ItemOrder",
                column: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_OrderType",
                table: "ItemOrder",
                column: "OrderType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyDealOrder");

            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "DailyDeal");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "OrderType");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
