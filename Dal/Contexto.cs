using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StockManager.Models;

namespace StockManager.Dal
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Marca> Marcas { get; set; } = null!;
        public DbSet<EstadoProducto> EstadosProductos { get; set; } = null!;
        public DbSet<Producto> productos{ get; set; } = null!;
        public DbSet<OrdenCompra> OrdenesCompras { get; set; } = null!;
        public DbSet<OrdenCompraDetalle> OrdenesComprasDetalles { get; set; } = null!;
        public DbSet<Factura> Facturas { get; set; } = null!;
        public DbSet<FacturaDetalle> FacturasDetalles { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<TipoUsuario> TiposUsuarios { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<EstadoCliente> EstadosClientes { get; set; } = null!;
        public DbSet<CuentaPorCobrar> CuentasPorCobrar { get; set; } = null!;
        public DbSet<RegistroPago> RegistrosPagos { get; set; } = null!;
        public DbSet<EstadoFactura> EstadosFacturas { get; set; } = null!;
        public DbSet<EstadoCuenta> EstadosCuentas { get; set; } = null!;
        public DbSet<EstadoOrdenCompra> EstadosOrdenCompra { get; set; } = null!;
        public DbSet<EstadoProveedor> EstadosProveedores { get; set; } = null!;
        public DbSet<Proveedor> Proveedores { get; set; } = null!;
        public DbSet<Notificacion> Notificaciones { get; set; } = null!;
        public DbSet<CuentaPorPagar> CuentasPorPagar { get; set; } = null!;
        public DbSet<Venta> Ventas { get; set; } = null!;
        public DbSet<DetalleVenta> VentasDetalles { get; set; } = null!;
        public DbSet<Mermitas> Mermas { get; set; } = null!;

        public Contexto() { }
    }
}