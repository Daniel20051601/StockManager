using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockManager.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentasPorCobrar_Cliente_ClienteId",
                table: "CuentasPorCobrar");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVentas_Ventas_VentaId",
                table: "DetallesVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVentas_productos_ProductoId",
                table: "DetallesVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Cliente_ClienteId",
                table: "Facturas");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Cliente_ClienteId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productos",
                table: "productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesVentas",
                table: "DetallesVentas");

            migrationBuilder.RenameTable(
                name: "productos",
                newName: "Productos");

            migrationBuilder.RenameTable(
                name: "DetallesVentas",
                newName: "DetallesVenta");

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

            migrationBuilder.RenameIndex(
                name: "IX_DetallesVentas_VentaId",
                table: "DetallesVenta",
                newName: "IX_DetallesVenta_VentaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesVentas_ProductoId",
                table: "DetallesVenta",
                newName: "IX_DetallesVenta_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesVenta",
                table: "DetallesVenta",
                column: "DetalleVentaId");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    RNCCedula = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    LimiteCredito = table.Column<decimal>(type: "numeric", nullable: false),
                    EstadoClienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Clientes_EstadosClientes_EstadoClienteId",
                        column: x => x.EstadoClienteId,
                        principalTable: "EstadosClientes",
                        principalColumn: "EstadoClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EstadoClienteId",
                table: "Clientes",
                column: "EstadoClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentasPorCobrar_Clientes_ClienteId",
                table: "CuentasPorCobrar",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_Productos_ProductoId",
                table: "DetallesVenta",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_Ventas_VentaId",
                table: "DetallesVenta",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Clientes_ClienteId",
                table: "Facturas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Clientes_ClienteId",
                table: "Ventas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentasPorCobrar_Clientes_ClienteId",
                table: "CuentasPorCobrar");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVenta_Productos_ProductoId",
                table: "DetallesVenta");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVenta_Ventas_VentaId",
                table: "DetallesVenta");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Clientes_ClienteId",
                table: "Facturas");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Clientes_ClienteId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesVenta",
                table: "DetallesVenta");

            migrationBuilder.RenameTable(
                name: "Productos",
                newName: "productos");

            migrationBuilder.RenameTable(
                name: "DetallesVenta",
                newName: "DetallesVentas");

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

            migrationBuilder.RenameIndex(
                name: "IX_DetallesVenta_VentaId",
                table: "DetallesVentas",
                newName: "IX_DetallesVentas_VentaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesVenta_ProductoId",
                table: "DetallesVentas",
                newName: "IX_DetallesVentas_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productos",
                table: "productos",
                column: "ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesVentas",
                table: "DetallesVentas",
                column: "DetalleVentaId");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EstadoClienteId = table.Column<int>(type: "integer", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    LimiteCredito = table.Column<decimal>(type: "numeric", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    RNCCedula = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Cliente_EstadosClientes_EstadoClienteId",
                        column: x => x.EstadoClienteId,
                        principalTable: "EstadosClientes",
                        principalColumn: "EstadoClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_EstadoClienteId",
                table: "Cliente",
                column: "EstadoClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentasPorCobrar_Cliente_ClienteId",
                table: "CuentasPorCobrar",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVentas_Ventas_VentaId",
                table: "DetallesVentas",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVentas_productos_ProductoId",
                table: "DetallesVentas",
                column: "ProductoId",
                principalTable: "productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Cliente_ClienteId",
                table: "Facturas",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Cliente_ClienteId",
                table: "Ventas",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
