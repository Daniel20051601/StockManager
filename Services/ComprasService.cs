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
        // Clear any tracked entities
        contexto.ChangeTracker.Clear();

        // Attach the order without its relationships first
        var entry = contexto.OrdenesCompras.Add(ordenCompra);

        // If needed, explicitly set foreign key values instead of navigation properties
        foreach (var detalle in ordenCompra.Detalles)
        {
            detalle.OrdenCompraId = ordenCompra.OrdenCompraId;
            // Ensure we only set the foreign key, not the full navigation property
            detalle.Producto = null;
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(OrdenCompra ordenCompra)
    {
        try
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            contexto.ChangeTracker.Clear();

            if (ordenCompra.OrdenCompraId == 0)
            {
                // For new orders, only set the foreign keys
                foreach (var detalle in ordenCompra.Detalles)
                {
                    var productoId = detalle.ProductoId;
                    detalle.Producto = null;  // Don't track the navigation property
                    detalle.ProductoId = productoId;
                }

                contexto.OrdenesCompras.Add(ordenCompra);
            }
            else
            {
                // For existing orders, update with careful navigation property handling
                var ordenExistente = await contexto.OrdenesCompras
                    .Include(o => o.Detalles)
                    .FirstOrDefaultAsync(o => o.OrdenCompraId == ordenCompra.OrdenCompraId);

                if (ordenExistente == null)
                    return false;

                // Update only scalar properties
                contexto.Entry(ordenExistente).CurrentValues.SetValues(ordenCompra);

                // Handle details separately
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
                        detalle.Producto = null;  // Don't track navigation property
                        ordenExistente.Detalles.Add(detalle);
                    }
                    else
                    {
                        contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
                    }
                }
            }

            return await contexto.SaveChangesAsync() > 0;
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

    public async Task<List<EstadoOrdenCompra>> EstadoOrdenCompras()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.EstadosOrdenCompra.AsNoTracking().ToListAsync();
    }

}
