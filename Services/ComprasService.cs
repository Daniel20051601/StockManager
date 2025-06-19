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
        try
        {
            if (ordenCompra.OrdenCompraId == 0)
            {
                return await Insertar(ordenCompra);
            }
            else
            {
                await using var contexto = await DbContext.CreateDbContextAsync();
                contexto.ChangeTracker.Clear();

                var ordenExistente = await contexto.OrdenesCompras
                    .Include(o => o.Detalles)
                    .FirstOrDefaultAsync(o => o.OrdenCompraId == ordenCompra.OrdenCompraId);

                if (ordenExistente == null)
                    return false;

                contexto.Entry(ordenExistente).CurrentValues.SetValues(ordenCompra);

                // Manejar detalles
                foreach (var detalleExistente in ordenExistente.Detalles.ToList())
                {
                    if (!ordenCompra.Detalles.Any(d => d.OrdenCompraDetalleId == detalleExistente.OrdenCompraDetalleId))
                    {
                        contexto.OrdenesComprasDetalles.Remove(detalleExistente);
                    }
                }

                foreach (var detalle in ordenCompra.Detalles)
                {
                    var detalleExistente = ordenExistente.Detalles
                        .FirstOrDefault(d => d.OrdenCompraDetalleId == detalle.OrdenCompraDetalleId);

                    if (detalleExistente == null)
                    {
                        ordenExistente.Detalles.Add(detalle);
                    }
                    else
                    {
                        contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
                    }
                }

                return await contexto.SaveChangesAsync() > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en Guardar: {ex}");
            return false;
        }
    }

    public async Task<OrdenCompra?> Buscar(int OrdenCompraId) 
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.OrdenesCompras
        .Include(p => p.Proveedor)
        .Include(u => u.Usuario)
        .Include(e => e.EstadoOrdenCompra) 
        .Include(o => o.Detalles)
            .ThenInclude(d => d.Producto)
                .ThenInclude(p => p.Marca)
        .FirstOrDefaultAsync(o => o.OrdenCompraId == OrdenCompraId);
    }

    public async Task<bool> Eliminar(int OrdenCompraId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        try
        {
            var orden = await contexto.OrdenesCompras
                .FirstOrDefaultAsync(o => o.OrdenCompraId == OrdenCompraId);

            if (orden == null)
                return false;

            orden.EstadoOrdenCompraId = 4; 
            contexto.OrdenesCompras.Update(orden);

            return await contexto.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al inactivar la orden: {ex}");
            return false;
        }
    }

    public async Task<List<OrdenCompra>> Listar(Expression<Func<OrdenCompra, bool>> criterio)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.OrdenesCompras
            .Include(p => p.Proveedor)
            .Include(u => u.Usuario)
            .Include(e => e.EstadoOrdenCompra)
            .Include(o => o.Detalles)
                    .ThenInclude(d => d.Producto)
                    .ThenInclude(p => p.Marca)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

}
