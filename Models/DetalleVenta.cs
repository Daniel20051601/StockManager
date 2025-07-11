using StockManager.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class DetalleVenta
{

    [ForeignKey("Venta")]
    public int VentaId { get; set; }
    public Venta Venta { get; set; }

    public int DetalleVentaId { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; }

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal => Cantidad * PrecioUnitario;
}
