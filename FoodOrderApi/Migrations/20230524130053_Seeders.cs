using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodOrderApi.Migrations
{
    /// <inheritdoc />
    public partial class Seeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "ProductID", "ProductName", "ProductPrice", "RestaurantID" },
                values: new object[,]
                {
                    { 1, 1, "Thai Broccoli Salad", "255", 1 },
                    { 2, 2, "Chettinad Cutlet", "275", 1 },
                    { 3, 3, "Curd Rice Aracini", "265", 1 },
                    { 4, 1, "Idly(2)", "33", 2 },
                    { 5, 2, "Pongal", "48", 2 },
                    { 6, 3, "Roast", "75", 2 },
                    { 7, 1, "Chocolate Sizzler", "299", 3 },
                    { 8, 2, "Choco Brownie Bomb", "249", 3 },
                    { 9, 3, "Chocolate Cookies", "199", 3 },
                    { 10, 1, "Vegetable Club Sandwich", "150", 4 },
                    { 11, 2, "Veg Combo Meal", "439", 4 },
                    { 12, 3, "Orbis Signature Rice", "250", 4 }
                });

            migrationBuilder.InsertData(
                table: "RestaurantWithMenus",
                columns: new[] { "RestaurantID", "Menus", "RestaurantLocation", "RestaurantName", "RestaurantPhoneNumber", "RestaurantType" },
                values: new object[,]
                {
                    { 1, "Thai Broccoli Salad,Curd Rice Aracini,Chettinad Cutlet\r\n", "RS Puram", "Kove", 1234567890, "Fine Dining" },
                    { 2, "Idly(2),Pongal,Roast\r\n", "RS Puram", "Annapoorna", 1234567890, "Casual Dining" },
                    { 3, "Chocolate Sizzler,Choco Brownie Bomb,Chocolate Cookies\r\n", "Race Course Road", "Chocolate Room", 1234567890, "Desserts and Beerages" },
                    { 4, "Vegetable Club Sandwich,Veg Combo Meal,Orbis Signature Rice\r\n", "Avinashi Road", "Orbis", 1234567890, "Multi Cuisine" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantID", "RestaurantLocation", "RestaurantName", "RestaurantPhoneNumber", "RestaurantType" },
                values: new object[,]
                {
                    { 1, "RS Puram", "Kove", 1234567890, "Fine Dining" },
                    { 2, "RS Puram", "Annapoorna", 1234567890, "Casual Dining" },
                    { 3, "Race Course Road", "Chocolate Room", 1234567890, "Desserts and Beerages" },
                    { 4, "Avinashi Road", "Orbis", 1234567890, "Multi Cuisine" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RestaurantWithMenus",
                keyColumn: "RestaurantID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RestaurantWithMenus",
                keyColumn: "RestaurantID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RestaurantWithMenus",
                keyColumn: "RestaurantID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RestaurantWithMenus",
                keyColumn: "RestaurantID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantID",
                keyValue: 4);
        }
    }
}
