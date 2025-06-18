using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StockManager.Dal;
using StockManager.Models;
using System.Linq.Expressions;

namespace StockManager.Services;

public class ComprasService(IDbContextFactory<Contexto> DbContext)
{
    public async Task<bool> Existe(int OrdenId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.OrdenesCompras.AnyAsync(o => o.OrdenCompraId == OrdenId);
    }

    public async Task<bool> Modificar(OrdenCompra ordenCompra)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.OrdenesCompras.Update(ordenCompra);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Insertar(OrdenCompra ordenCompra)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.OrdenesCompras.Add(ordenCompra);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(OrdenCompra ordenCompra)
    {
        if(!await Existe(ordenCompra.OrdenCompraId))
        {
            return await Insertar(ordenCompra);
        }
        else
        {
            return await Modificar(ordenCompra);
        }
    }

    public async Task<OrdenCompra?> Buscar(int OrdenCompraId) 
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.OrdenesCompras
            .Include(p =>p.Proveedor)
            .Include(u =>u.Usuario)
            .Include(e => e.EstadoOrdenCompraId )
            .FirstOrDefaultAsync(o => o.OrdenCompraId == OrdenCompraId);
    }

    public async Task<bool> Eliminar(int OrdenCompraId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.OrdenesCompras
            .AsNoTracking()
            .Where(t => t.OrdenCompraId == OrdenCompraId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<OrdenCompra>> Listar(Expression<Func<OrdenCompra, bool>> criterio)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.OrdenesCompras
            .Include(p => p.Proveedor)
            .Include(u => u.Usuario)
            .Include(e => e.EstadoOrdenCompra)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

}
