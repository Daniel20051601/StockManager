using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class Reporte
{
    [Key]
    public int ReporteId { get; set; }

    [ForeignKey("Proveedor")]
    public int ProveedorId { get; set; }

    [Required, MaxLength(200)]
    public string Asunto { get; set; } = null!;

    [Required, MaxLength(1000)]
    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Navegación
    public Proveedor Proveedor { get; set; } = null!;
}