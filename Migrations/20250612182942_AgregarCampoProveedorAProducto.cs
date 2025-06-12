using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManager.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoProveedorAProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_Categorias_CategoriaId",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK_productos_Marcas_MarcaId",
                table: "productos");

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "productos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "productos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Proveedor",
                table: "productos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Categorias_CategoriaId",
                table: "productos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Marcas_MarcaId",
                table: "productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_Categorias_CategoriaId",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK_productos_Marcas_MarcaId",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "Proveedor",
                table: "productos");

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "productos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "productos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Categorias_CategoriaId",
                table: "productos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Marcas_MarcaId",
                table: "productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
