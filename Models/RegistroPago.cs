using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class RegistroPago
{
    [Key]
    public int PagoId { get; set; }

    public decimal Monto { get; set; }
    public DateTime FechaPago { get; set; } = DateTime.Now;
    public string MetodoPago { get; set; } = null!; 

    [ForeignKey("CuentaPorPagar")]
    public int? CuentaPorPagarId { get; set; }
    public CuentaPorPagar CuentaPorPagar { get; set; } = null!;

    [ForeignKey("CuentaPorCobrar")]
    public int? CuentaPorCobrarId { get; set; }
    public CuentaPorCobrar CuentaPorCobrar { get; set; } = null!;

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
