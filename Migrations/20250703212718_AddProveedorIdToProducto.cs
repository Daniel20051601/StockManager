using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManager.Migrations
{
    /// <inheritdoc />
    public partial class AddProveedorIdToProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Proveedor",
                table: "productos");

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "productos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_productos_ProveedorId",
                table: "productos",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Proveedores_ProveedorId",
                table: "productos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_Proveedores_ProveedorId",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_ProveedorId",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "productos");

            migrationBuilder.AddColumn<string>(
                name: "Proveedor",
                table: "productos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
