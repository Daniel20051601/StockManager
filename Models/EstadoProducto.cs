using System.ComponentModel.DataAnnotations;

namespace StockManager.Models
{
    public class EstadoProducto
    {
        [Key]
        public int EstadoProductoId { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;

        public ICollection<Producto> Productos { get; set; } = null!;
    }
}
