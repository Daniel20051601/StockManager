using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StockManager.Models;

namespace StockManager.Dal
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        
        public DbSet<Productos> Productos { get; set; }

        public Contexto() { }
    }
}
