using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProducProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaPrice",
                table: "Inventories",
                newName: "AveragePrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AveragePrice",
                table: "Inventories",
                newName: "MediaPrice");
        }
    }
}
