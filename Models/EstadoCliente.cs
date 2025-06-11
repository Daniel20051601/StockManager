using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class EstadoCliente
{
    [Key]
    public int EstadoClienteId { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = null!;

    public ICollection<Cliente> Clientes { get; set; } = null!;
}
