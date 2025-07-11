using StockManager.Models;

public class Venta
{
    public int VentaId { get; set; }
    public int ClienteId { get; set; }
    public Clientes Clientes { get; set; }

    public decimal Total { get; set; }
    public DateTime Fecha { get; set; }

    public List<DetalleVenta> Detalles { get; set; } = new();

}
