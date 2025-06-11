using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class EstadoOrdenCompra
{
    [Key]
    public int EstadoOrdenCompraId { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = null!;

    public ICollection<OrdenCompra> OrdenesCompra { get; set; } = null!;
}
