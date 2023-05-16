using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderApi.Migrations
{
    /// <inheritdoc />
    public partial class ConfigFoodOrderApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Orders",
                newName: "ProductID (Numbers)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID (Numbers)",
                table: "Orders",
                newName: "ProductID");
        }
    }
}
