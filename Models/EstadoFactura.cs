using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class EstadoFactura
{
    [Key]
    public int EstadoFacturaId { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = null!;

    public ICollection<Factura> Facturas { get; set; } = null!;
}
