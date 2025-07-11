using Microsoft.EntityFrameworkCore;
using StockManager.Models;
using StockManager.Dal;

namespace StockManager.Services
{
    public class ClienteService
    {
        private readonly IDbContextFactory<Contexto> _dbContextFactory;

        public ClienteService(IDbContextFactory<Contexto> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Clientes>> ObtenerClientesAsync()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Clientes.ToListAsync();
        }

        public async Task<List<Clientes>> ListarAsync()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking().ToListAsync();
        }
        public async Task<List<Clientes>> ObtenerTodos()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Clientes.ToListAsync();  // Obtiene todos los clientes de la base de datos
        }

    }
}
