using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }
}
