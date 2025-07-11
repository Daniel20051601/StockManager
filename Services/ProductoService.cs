using Microsoft.EntityFrameworkCore;
using StockManager.Dal;
using StockManager.Models;
using System.Linq.Expressions;

namespace StockManager.Services;

public class ProductoService
{
    private readonly IDbContextFactory<Contexto> _dbContextFactory;

    public ProductoService(IDbContextFactory<Contexto> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory; // Asigna el parámetro al campo de instancia
    }

    public async Task<bool> Existe(int productoId)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.productos.AnyAsync(p => p.ProductoId == productoId);
    }

    public async Task<Producto?> GetProductoById(int productoId)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.productos
                             .Include(p => p.Categoria)
                             .Include(p => p.Marca)
                             .FirstOrDefaultAsync(p => p.ProductoId == productoId);
    }

    public async Task<bool> Modificar(Producto producto)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        contexto.productos.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Insertar(Producto producto)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        contexto.productos.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Producto producto)
    {
        if (producto.ProductoId == 0)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
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
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        var producto = await contexto.productos.FindAsync(productoId);
        if (producto == null)
            return false;

        contexto.productos.Remove(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> criterio)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.productos
            .Include(m => m.Marca)
            .Include(c => c.Categoria)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Producto>> ListarTodosAsync()
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.productos
                             .Include(p => p.Categoria)
                             .Include(p => p.Marca)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<bool> ActualizarStockAsync(List<DetalleVenta> detalles)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();

        foreach (var detalle in detalles)
        {
            var producto = await contexto.productos.FindAsync(detalle.ProductoId);
            if (producto != null)
            {
                if (producto.Stock < detalle.Cantidad)
                    return false; // Opcional: puedes lanzar excepción o mostrar error si no hay suficiente stock

                producto.Stock -= detalle.Cantidad;
            }
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Producto>> ListarDisponiblesAsync()
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.productos
                             .Where(p => p.Stock > 0)
                             .Include(p => p.Marca)
                             .Include(p => p.Categoria)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<List<Producto>> ObtenerTodos()
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.productos.ToListAsync();  // Obtiene todos los productos de la base de datos
    }

    public async Task<List<Producto>> BuscarProductosAsync(string busqueda)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();

        // Buscar productos cuyo nombre contenga el texto ingresado (ignorando mayúsculas/minúsculas)
        return await contexto.productos
                             .Where(p => p.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase))
                             .Include(p => p.Marca)
                             .Include(p => p.Categoria)
                             .Take(10)  // Limitar a los primeros 10 resultados
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<bool> ActualizarProductoAsync(Producto producto)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();

        var productoExistente = await contexto.productos.FindAsync(producto.ProductoId);
        if (productoExistente == null)
            return false;

        // Actualizar solo los campos necesarios (por ejemplo, el stock)
        productoExistente.Stock = producto.Stock;

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Producto>> BuscarDisponiblesAsync(string busqueda)
    {
        // Si la búsqueda está vacía, retornamos una lista vacía
        if (string.IsNullOrWhiteSpace(busqueda))
        {
            return new List<Producto>();
        }

        await using var contexto = await _dbContextFactory.CreateDbContextAsync();

        // Convertir la búsqueda a minúsculas para hacer la búsqueda insensible a mayúsculas/minúsculas
        var filtro = busqueda.Trim().ToLower();

        // Filtrar los productos que contengan el nombre o el código que coincidan con la búsqueda y tengan stock
        var productos = await contexto.productos
            .Where(p => (p.Nombre.ToLower().Contains(filtro) || p.Codigo.ToLower().Contains(filtro)) && p.Stock > 0)
            .Include(p => p.Marca)
            .Include(p => p.Categoria)
            .AsNoTracking()
            .ToListAsync();

        return productos;
    }

    public async Task<Producto> GetProductoByIdAsync(int productoId)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.Set<Producto>() // Utiliza contexto en lugar de _context
            .FirstOrDefaultAsync(p => p.ProductoId == productoId);
    }

    public async Task<bool> HaySuficienteStockAsync(int productoId, int cantidad)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync(); // Usa _dbContextFactory aquí
        var producto = await contexto.productos
            .Where(p => p.ProductoId == productoId)
            .FirstOrDefaultAsync();

        // Verifica si el producto existe y si hay suficiente stock
        return producto != null && producto.Stock >= cantidad;
    }
}
