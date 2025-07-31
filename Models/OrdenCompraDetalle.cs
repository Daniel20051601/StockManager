using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class OrdenCompraDetalle
{
    [Key]
    public int OrdenCompraDetalleId { get; set; }

    [ForeignKey("OrdenCompra")]
    public int OrdenCompraId { get; set; }
    public OrdenCompra OrdenCompra { get; set; } = null!;

    [ForeignKey("Producto")]
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;

    [Range(1, 1000, ErrorMessage = "La cantidad debe ser mayor que 1 y menor que 1000")]
    public int Cantidad { get; set; }

   
    public decimal PrecioCompraUnitario { get; set; }

    public decimal Subtotal { get; set; }
}
