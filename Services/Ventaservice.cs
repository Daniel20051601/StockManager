using Microsoft.EntityFrameworkCore;
using StockManager.Dal;
using StockManager.Models;

namespace StockManager.Services;

public class VentaService(IDbContextFactory<Contexto> dbContext, ProductoService productoService)
{
    public async Task<bool> GuardarVentaAsync(Venta venta)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();

        // Establecer fecha actual en UTC
        venta.Fecha = DateTime.UtcNow;

        // Adjuntar productos existentes y asignar precios
        foreach (var detalle in venta.Detalles)
        {
            var producto = await contexto.productos.FindAsync(detalle.ProductoId);
            if (producto == null)
                return false;

            detalle.PrecioUnitario = producto.PrecioVenta;
        }

        contexto.Ventas.Add(venta);

        var ventaGuardada = await contexto.SaveChangesAsync() > 0;

        if (ventaGuardada)
        {
            // Actualizar el stock de los productos
        }

        return false;
    }

    public async Task<List<Venta>> ListarAsync()
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Ventas
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Venta?> ObtenerPorIdAsync(int ventaId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Ventas
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.VentaId == ventaId);
    }

    // Método corregido para obtener ventas del día en UTC
    public async Task<int> GetVentasDiarias()
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        var fechaHoyUtc = DateTime.UtcNow.Date;
        return await contexto.Ventas.CountAsync(v => v.Fecha.Date == fechaHoyUtc);
    }
}
