using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class Notificacion
{
    [Key]
    public int NotificacionId { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public string Mensaje { get; set; } = null!;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public bool Leida { get; set; } = false;
}

