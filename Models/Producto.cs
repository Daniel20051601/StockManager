using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [MaxLength(50)]
        public string? Codigo { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = null!;
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        
        public Proveedor Proveedor { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        [ForeignKey("Marca")]
        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = null!;

        public int Stock { get; set; } = 0;
        public int StockMinimo { get; set; } = 0;

        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }

        public string? ImagenURL { get; set; }

        [ForeignKey("EstadoProducto")]
        public int EstadoProductoId { get; set; } = 1; 
        public EstadoProducto EstadoProducto { get; set; } = null!;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<OrdenCompraDetalle> OrdenesDetalle { get; set; } = null!;
        public ICollection<FacturaDetalle> FacturaDetalles { get; set; } = null!;
    }
}
