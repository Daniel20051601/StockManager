using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.Models;

public class Reporte
{
    [Key]
    public int ReporteId { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un proveedor")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor")]
    [ForeignKey("Proveedor")]
    public int ProveedorId { get; set; }

    [Required(ErrorMessage = "El asunto es requerido")]
    [MaxLength(200, ErrorMessage = "El asunto no puede exceder 200 caracteres")]
    public string Asunto { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es requerida")]
    [MaxLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

   
    public Proveedor? Proveedor { get; set; }
}