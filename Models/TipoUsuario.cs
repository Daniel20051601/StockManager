using System.ComponentModel.DataAnnotations;

namespace StockManager.Models;

public class TipoUsuario
{
    [Key]
    public int TipoUsuarioId { get; set; }

    [Required, MaxLength(50)]
    public string NombreRol { get; set; } = null!;

    public ICollection<Usuario> Usuarios { get; set; } = null!;
}
