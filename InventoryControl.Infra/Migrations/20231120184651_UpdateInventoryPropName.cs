using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInventoryPropName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AveragePrice",
                table: "Inventories",
                newName: "TotalValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalValue",
                table: "Inventories",
                newName: "AveragePrice");
        }
    }
}
