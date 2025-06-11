using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class CuentaPorPagar
{
    [Key]
    public int CuentaPorPagarId { get; set; }

    [ForeignKey("OrdenCompra")]
    public int OrdenCompraId { get; set; }
    public OrdenCompra OrdenCompra { get; set; } = null!;

    public decimal MontoTotal { get; set; }
    public decimal SaldoPendiente { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaLimitePago { get; set; }

    [ForeignKey("EstadoCuenta")]
    public int EstadoCuentaId { get; set; }
    public EstadoCuenta EstadoCuenta { get; set; } = null!;

    public ICollection<RegistroPago> Pagos { get; set; } = null!;
}
