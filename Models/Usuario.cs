using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = null!;

    [Required, MaxLength(50)]
    public string NombreUsuario { get; set; } = null!;

    [Required]
    public string Contraseña { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [ForeignKey("TipoUsuario")]
    public int TipoUsuarioId { get; set; }
    public TipoUsuario TipoUsuario { get; set; } = null!;

    public string FotoURL { get; set; } = null!;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public bool Estado { get; set; } = true;

    public ICollection<OrdenCompra> OrdenesCompra { get; set; } = null!;
    public ICollection<Factura> Facturas { get; set; } = null!;
    public ICollection<RegistroPago> PagosRegistrados { get; set; } = null!;
}
