using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class Factura
{
    [Key]
    public int FacturaId { get; set; }

    [Required, MaxLength(50)]
    public string NumeroFactura { get; set; } = null!;

    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public DateTime FechaEmision { get; set; } = DateTime.Now;
    public decimal Subtotal { get; set; }
    public decimal Descuento { get; set; }
    public decimal Impuestos { get; set; }
    public decimal Total { get; set; }

    [ForeignKey("EstadoFactura")]
    public int EstadoFacturaId { get; set; }
    public EstadoFactura EstadoFactura { get; set; } = null!;

    public string TipoPago { get; set; } = null!;

    public ICollection<FacturaDetalle> Detalles { get; set; } = null!;
}
