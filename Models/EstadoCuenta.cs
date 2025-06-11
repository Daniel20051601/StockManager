using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class EstadoCuenta
{

    [Key]
    public int EstadoCuentaId { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = null!;

    public ICollection<CuentaPorPagar> CuentasPorPagar { get; set; } = null!;
    public ICollection<CuentaPorCobrar> CuentasPorCobrar { get; set; } = null!;
}
