using Microsoft.EntityFrameworkCore;
using StockManager.Dal;
using StockManager.Models;

namespace StockManager.Services;

public class ProductoService(IDbContextFactory<Contexto> DbContext)
{
    public async Task<bool> Existe(int productoId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.productos.AnyAsync(p => p.ProductoId == productoId);
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
        if(await Existe(producto.ProductoId))
        {
            return await Modificar(producto);
        }
        else
        {
            return await Insertar(producto);
        }
            
    }
}
