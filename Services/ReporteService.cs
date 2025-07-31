using StockManager.Dal;
using StockManager.Models;
using Microsoft.EntityFrameworkCore;

namespace StockManager.Services
{
    public class ReporteService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public ReporteService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Reporte>> GetReportesRecientesAsync(int count)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Reportes
                .Include(r => r.Proveedor)
                .OrderByDescending(r => r.FechaCreacion)
                .Take(count)
                .ToListAsync();
        }
    }
}
