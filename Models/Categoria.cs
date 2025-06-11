using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public ICollection<Producto> Productos { get; set; } = null!;
}
