using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class CuentaPorCobrar
{
    [Key]
    public int CuentaPorCobrarId { get; set; }

    [ForeignKey("Factura")]
    public int FacturaId { get; set; }
    public Factura Factura { get; set; } = null!;

    [ForeignKey("Clientes")]
    public int ClienteId { get; set; }
    public Clientes Clientes { get; set; } = null!;

    public decimal MontoTotal { get; set; }
    public decimal SaldoPendiente { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaLimitePago { get; set; }
    public decimal Mora { get; set; }

    [ForeignKey("EstadoCuenta")]
    public int EstadoCuentaId { get; set; }
    public EstadoCuenta EstadoCuenta { get; set; } = null!;

    public ICollection<RegistroPago> Pagos { get; set; } = null!;
}
