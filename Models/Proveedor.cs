using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class Proveedor
{
    [Key]
    public int ProveedorId { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; } = null!;

    public string RNC { get; set; } = null!;
    public string NombreContacto { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Direccion { get; set; } = null!;

    [ForeignKey("EstadoProveedor")]
    public int EstadoProveedorId { get; set; }
    public EstadoProveedor EstadoProveedor { get; set; } = null!;

    public ICollection<OrdenCompra> OrdenesCompra { get; set; } = null!;
}
