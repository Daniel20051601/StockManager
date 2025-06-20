using Microsoft.EntityFrameworkCore;
using StockManager.Dal;
using StockManager.Models;
using System.Linq.Expressions;

namespace StockManager.Services;

public class ProductoService(IDbContextFactory<Contexto> DbContext)
{
    public async Task<bool> Existe(int productoId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.productos.AnyAsync(p => p.ProductoId == productoId);
    }

    public async Task<Producto?> GetProductoById(int productoId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.productos
                             .Include(p => p.Categoria)
                             .Include(p => p.Marca)
                             .FirstOrDefaultAsync(p => p.ProductoId == productoId);
    }

    public async Task<bool> Modificar(Producto producto)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.productos.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Insertar(Producto producto)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.productos.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Producto producto)
    {
        if (producto.ProductoId == 0)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            contexto.productos.Add(producto);
            await contexto.SaveChangesAsync();
            return true;
        }
        else
        {
            return await Modificar(producto);
        }
    }

    public async Task<bool> Eliminar(int productoId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        var producto = await contexto.productos.FindAsync(productoId);
        if (producto == null)
            return false;

        contexto.productos.Remove(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> criterio)
    {
        using var contexto = DbContext.CreateDbContext();
        return await contexto.productos
            .Include(m => m.Marca)
            .Include(c => c.Categoria)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}