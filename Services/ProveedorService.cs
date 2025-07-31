using Microsoft.EntityFrameworkCore;
using StockManager.Dal;
using StockManager.Models;
using System.Linq.Expressions;

namespace StockManager.Services
{
    public class ProveedorService(IDbContextFactory<Contexto> DbContext)
    {
        public async Task<List<Proveedor>> Listar(Expression<Func<Proveedor, bool>> criterio)
        {
            using var contexto = DbContext.CreateDbContext();
            return await contexto.Proveedores
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
