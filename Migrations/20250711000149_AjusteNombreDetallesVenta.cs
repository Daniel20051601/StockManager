using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManager.Migrations
{
    /// <inheritdoc />
    public partial class AjusteNombreDetallesVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVenta_Productos_ProductoId",
                table: "DetallesVenta");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturasDetalles_Productos_ProductoId",
                table: "FacturasDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesComprasDetalles_Productos_ProductoId",
                table: "OrdenesComprasDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_EstadosProductos_EstadoProductoId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.RenameTable(
                name: "Productos",
                newName: "productos");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_ProveedorId",
                table: "productos",
                newName: "IX_productos_ProveedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_MarcaId",
                table: "productos",
                newName: "IX_productos_MarcaId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_EstadoProductoId",
                table: "productos",
                newName: "IX_productos_EstadoProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_CategoriaId",
                table: "productos",
                newName: "IX_productos_CategoriaId");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "DetallesVenta",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_productos",
                table: "productos",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_productos_ProductoId",
                table: "DetallesVenta",
                column: "ProductoId",
                principalTable: "productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasDetalles_productos_ProductoId",
                table: "FacturasDetalles",
                column: "ProductoId",
                principalTable: "productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesComprasDetalles_productos_ProductoId",
                table: "OrdenesComprasDetalles",
                column: "ProductoId",
                principalTable: "productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Categorias_CategoriaId",
                table: "productos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productos_EstadosProductos_EstadoProductoId",
                table: "productos",
                column: "EstadoProductoId",
                principalTable: "EstadosProductos",
                principalColumn: "EstadoProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Marcas_MarcaId",
                table: "productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_DetallesVenta_productos_ProductoId",
                table: "DetallesVenta");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturasDetalles_productos_ProductoId",
                table: "FacturasDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesComprasDetalles_productos_ProductoId",
                table: "OrdenesComprasDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_productos_Categorias_CategoriaId",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK_productos_EstadosProductos_EstadoProductoId",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK_productos_Marcas_MarcaId",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK_productos_Proveedores_ProveedorId",
                table: "productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productos",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "DetallesVenta");

            migrationBuilder.RenameTable(
                name: "productos",
                newName: "Productos");

            migrationBuilder.RenameIndex(
                name: "IX_productos_ProveedorId",
                table: "Productos",
                newName: "IX_Productos_ProveedorId");

            migrationBuilder.RenameIndex(
                name: "IX_productos_MarcaId",
                table: "Productos",
                newName: "IX_Productos_MarcaId");

            migrationBuilder.RenameIndex(
                name: "IX_productos_EstadoProductoId",
                table: "Productos",
                newName: "IX_Productos_EstadoProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_productos_CategoriaId",
                table: "Productos",
                newName: "IX_Productos_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_Productos_ProductoId",
                table: "DetallesVenta",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasDetalles_Productos_ProductoId",
                table: "FacturasDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesComprasDetalles_Productos_ProductoId",
                table: "OrdenesComprasDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_EstadosProductos_EstadoProductoId",
                table: "Productos",
                column: "EstadoProductoId",
                principalTable: "EstadosProductos",
                principalColumn: "EstadoProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId",
                table: "Productos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
