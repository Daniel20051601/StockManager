using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace StockManager.Models;

public class Mermas
{

    [Key]
    public int MermaId { get; set; }

    [Required]
    public int ProductoId { get; set; }

    [ForeignKey("ProductoId")]
    public Producto? Producto { get; set; } // Propiedad de navegación

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "La razón es requerida.")]
    [StringLength(500, ErrorMessage = "La razón no puede exceder 500 caracteres.")]
    public string? Razon { get; set; }

    public DateTime FechaMerma { get; set; } = DateTime.UtcNow;
}