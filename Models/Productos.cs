using System.ComponentModel.DataAnnotations;

namespace StockManager.Models
{
    public class Productos 
    {
        [Key]
        public int ProductoId { get; set; } 

        
        public string Codigo { get; set; }
        public string UrlImagen { get; set; } 
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string Proveedor { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }

        
    }
}