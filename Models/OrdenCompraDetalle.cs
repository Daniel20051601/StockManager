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

    public int Cantidad { get; set; }
    public decimal PrecioCompraUnitario { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal Subtotal { get; private set; }
}
