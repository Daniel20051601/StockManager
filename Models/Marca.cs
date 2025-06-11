using System.ComponentModel.DataAnnotations;
using static StockManager.Components.Pages.Ventas.VentasIndex;

namespace StockManager.Models;

public class Marca
{
    [Key]
    public int MarcaId { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = null!;

    public ICollection<Producto> Productos { get; set; } = null!;
}
