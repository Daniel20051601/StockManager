using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class OrdenCompra
{
    [Key]
    public int OrdenCompraId { get; set; }

    [Required(ErrorMessage ="Debe agregar un nombre de para la Orden de Compra")]
    public string Titulo { get; set; } = null!;

    [Required, MaxLength(50)]
    public string NumeroOrden { get; set; } = null!;

    [ForeignKey("Proveedor")]
    public int ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; } = null!;

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    [ForeignKey("EstadoOrdenCompra")]
    public int EstadoOrdenCompraId { get; set; }
    public EstadoOrdenCompra EstadoOrdenCompra { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    [Required]
    public DateTime FechaEntregaEstimada { get; set; }  = DateTime.Now;

    public decimal Subtotal { get; set; }
    public decimal Impuestos { get; set; }
    public decimal Total { get; set; }

    public string Notas { get; set; } = null!;

    public ICollection<OrdenCompraDetalle> Detalles { get; set; } = null!;
}
