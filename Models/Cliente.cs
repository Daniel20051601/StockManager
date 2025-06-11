using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; } = null!;

    public string RNCCedula { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public decimal LimiteCredito { get; set; } = 0;

    [ForeignKey("EstadoCliente")]
    public int EstadoClienteId { get; set; }
    public EstadoCliente EstadoCliente { get; set; } = null!;

    public ICollection<Factura> Facturas { get; set; } = null!;
    public ICollection<CuentaPorCobrar> CuentasPorCobrar { get; set; } = null!;
}
