using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodOrderApi.Migrations
{
    /// <inheritdoc />
    public partial class initalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RestaurantID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductPrice = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RestaurantName = table.Column<string>(type: "TEXT", nullable: false),
                    RestaurantType = table.Column<string>(type: "TEXT", nullable: false),
                    RestaurantPhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    RestaurantLocation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantID);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantWithMenus",
                columns: table => new
                {
                    RestaurantID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RestaurantName = table.Column<string>(type: "TEXT", nullable: false),
                    RestaurantType = table.Column<string>(type: "TEXT", nullable: false),
                    RestaurantPhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    RestaurantLocation = table.Column<string>(type: "TEXT", nullable: false),
                    Menus = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantWithMenus", x => x.RestaurantID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RestaurantID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductIDNumbers = table.Column<Guid>(name: "ProductID (Numbers)", type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Menus_ProductID (Numbers)",
                        column: x => x.ProductIDNumbers,
                        principalTable: "Menus",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Restaurants_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "ProductID", "ProductName", "ProductPrice", "RestaurantID" },
                values: new object[,]
                {
                    { new Guid("1ff36c1a-3e10-4651-88e4-7fc4736bf4d8"), "Chocolate Sizzler", "299", new Guid("7a03ca50-719f-48e7-ae0e-e4e299f3112b") },
                    { new Guid("316dc710-146c-430b-9b0f-e9aa51b1e8e9"), "Choco Brownie Bomb", "249", new Guid("7a03ca50-719f-48e7-ae0e-e4e299f3112b") },
                    { new Guid("3a011230-db41-4407-9218-347f51391818"), "Chocolate Cookies", "199", new Guid("7a03ca50-719f-48e7-ae0e-e4e299f3112b") },
                    { new Guid("5849b06f-9731-4e31-a908-6e7e9dafdd5a"), "Orbis Signature Rice", "250", new Guid("2818b184-94be-4a91-a3b8-a510b00bd6f5") },
                    { new Guid("780717ad-8473-4c23-b9b0-a256fd80f13f"), "Pongal", "48", new Guid("7e83a461-5a28-4b11-83a5-31458449b7ac") },
                    { new Guid("a91be131-8704-45ae-9ba7-60e70c58abb4"), "Veg Combo Meal", "439", new Guid("2818b184-94be-4a91-a3b8-a510b00bd6f5") },
                    { new Guid("be5a857b-1336-4d13-a1d8-b0c9683689ca"), "Idly(2)", "33", new Guid("7e83a461-5a28-4b11-83a5-31458449b7ac") },
                    { new Guid("c8e03d8c-3438-4583-b12e-1ba54c3670d6"), "Vegetable Club Sandwich", "150", new Guid("2818b184-94be-4a91-a3b8-a510b00bd6f5") },
                    { new Guid("e0194510-6ad1-4ac2-bf31-e572caa09ba1"), "Thai Broccoli Salad", "255", new Guid("baad586a-accf-4433-98f0-2f861e683354") },
                    { new Guid("e1764da4-941d-463a-8531-43b701306780"), "Roast", "75", new Guid("7e83a461-5a28-4b11-83a5-31458449b7ac") },
                    { new Guid("f2612c12-b189-4d23-9532-e7c07a1eebe3"), "Chettinad Cutlet", "275", new Guid("baad586a-accf-4433-98f0-2f861e683354") },
                    { new Guid("f613a27f-93c2-4028-9deb-ddc4b8b45e3a"), "Curd Rice Aracini", "265", new Guid("baad586a-accf-4433-98f0-2f861e683354") }
                });

            migrationBuilder.InsertData(
                table: "RestaurantWithMenus",
                columns: new[] { "RestaurantID", "Menus", "RestaurantLocation", "RestaurantName", "RestaurantPhoneNumber", "RestaurantType" },
                values: new object[,]
                {
                    { new Guid("2818b184-94be-4a91-a3b8-a510b00bd6f5"), "Vegetable Club Sandwich,Veg Combo Meal,Orbis Signature Rice\r\n", "Avinashi Road", "Orbis", 1234567890, "Multi Cuisine" },
                    { new Guid("7a03ca50-719f-48e7-ae0e-e4e299f3112b"), "Chocolate Sizzler,Choco Brownie Bomb,Chocolate Cookies\r\n", "Race Course Road", "Chocolate Room", 1234567890, "Desserts and Beerages" },
                    { new Guid("7e83a461-5a28-4b11-83a5-31458449b7ac"), "Idly(2),Pongal,Roast\r\n", "RS Puram", "Annapoorna", 1234567890, "Casual Dining" },
                    { new Guid("baad586a-accf-4433-98f0-2f861e683354"), "Thai Broccoli Salad,Curd Rice Aracini,Chettinad Cutlet\r\n", "RS Puram", "Kove", 1234567890, "Fine Dining" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantID", "RestaurantLocation", "RestaurantName", "RestaurantPhoneNumber", "RestaurantType" },
                values: new object[,]
                {
                    { new Guid("2818b184-94be-4a91-a3b8-a510b00bd6f5"), "Avinashi Road", "Orbis", 1234567890, "Multi Cuisine" },
                    { new Guid("7a03ca50-719f-48e7-ae0e-e4e299f3112b"), "Race Course Road", "Chocolate Room", 1234567890, "Desserts and Beerages" },
                    { new Guid("7e83a461-5a28-4b11-83a5-31458449b7ac"), "RS Puram", "Annapoorna", 1234567890, "Casual Dining" },
                    { new Guid("baad586a-accf-4433-98f0-2f861e683354"), "RS Puram", "Kove", 1234567890, "Fine Dining" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductID (Numbers)",
                table: "Orders",
                column: "ProductID (Numbers)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantID",
                table: "Orders",
                column: "RestaurantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "RestaurantWithMenus");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
