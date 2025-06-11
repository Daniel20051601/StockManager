using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class EstadoProveedor
{
    [Key]
    public int EstadoProveedorId { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = null!;

    public ICollection<Proveedor> Proveedores { get; set; } = null!;
}
