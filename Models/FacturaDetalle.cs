using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class FacturaDetalle
{
    [Key]
    public int FacturaDetalleId { get; set; }

    [ForeignKey("Factura")]
    public int FacturaId { get; set; }
    public Factura Factura { get; set; } = null!;

    [ForeignKey("Producto")]
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
     
    public int Cantidad { get; set; }
    public decimal PrecioVentaUnitario { get; set; }
    public decimal Descuento { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal Subtotal { get; private set; }
}
