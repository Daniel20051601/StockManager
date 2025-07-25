using Microsoft.EntityFrameworkCore;
using StockManager.Dal;
using StockManager.Models;

namespace StockManager.Services
{
    public class CuentasPorPagarService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public CuentasPorPagarService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        // Listar todas las cuentas
        public async Task<List<CuentaPorPagar>> ObtenerCuentasAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.CuentasPorPagar
                .Include(c => c.OrdenCompra)
                    .ThenInclude(o => o.Proveedor)
                .Include(c => c.EstadoCuenta)
                .OrderByDescending(c => c.FechaEmision)
                .ToListAsync();
        }

        // Obtener una cuenta por Id (con relaciones)
        public async Task<CuentaPorPagar?> ObtenerCuentaPorIdAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.CuentasPorPagar
                .Include(c => c.OrdenCompra)
                    .ThenInclude(o => o.Proveedor)
                .Include(c => c.EstadoCuenta)
                .FirstOrDefaultAsync(c => c.CuentaPorPagarId == id);
        }

        // Crear nueva cuenta
        public async Task<(bool Exito, string Mensaje)> CrearCuentaAsync(CuentaPorPagar cuenta)
        {
            using var context = _dbFactory.CreateDbContext();

            bool existe = await context.CuentasPorPagar
                .AnyAsync(c => c.OrdenCompraId == cuenta.OrdenCompraId);

            if (existe)
                return (false, "Ya existe una cuenta para esta orden de compra.");

            cuenta.FechaEmision = DateTime.SpecifyKind(cuenta.FechaEmision, DateTimeKind.Utc);
            cuenta.FechaLimitePago = DateTime.SpecifyKind(cuenta.FechaLimitePago, DateTimeKind.Utc);

            context.CuentasPorPagar.Add(cuenta);
            await context.SaveChangesAsync();
            return (true, "Cuenta creada correctamente.");
        }

        // Actualizar cuenta existente
        public async Task<(bool Exito, string Mensaje)> ActualizarCuentaAsync(CuentaPorPagar cuenta)
        {
            using var context = _dbFactory.CreateDbContext();
            var entidad = await context.CuentasPorPagar.FindAsync(cuenta.CuentaPorPagarId);

            if (entidad == null)
                return (false, "La cuenta no existe.");

            entidad.OrdenCompraId = cuenta.OrdenCompraId;
            entidad.MontoTotal = cuenta.MontoTotal;
            entidad.SaldoPendiente = cuenta.SaldoPendiente;
            entidad.FechaEmision = DateTime.SpecifyKind(cuenta.FechaEmision, DateTimeKind.Utc);
            entidad.FechaLimitePago = DateTime.SpecifyKind(cuenta.FechaLimitePago, DateTimeKind.Utc);
            entidad.EstadoCuentaId = cuenta.EstadoCuentaId;

            await context.SaveChangesAsync();
            return (true, "Cuenta actualizada correctamente.");
        }

        // Eliminar cuenta
        public async Task<bool> EliminarCuentaAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var entidad = await context.CuentasPorPagar.FindAsync(id);
            if (entidad == null) return false;

            context.CuentasPorPagar.Remove(entidad);
            await context.SaveChangesAsync();
            return true;
        }

        // Listar órdenes de compra (para los combos)
        public async Task<List<OrdenCompra>> ObtenerOrdenesCompraAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.OrdenesCompras
                .Include(o => o.Proveedor)
                .ToListAsync();
        }

        // Listar estados de cuenta (para los combos)
        public async Task<List<EstadoCuenta>> ObtenerEstadosCuentasAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.EstadosCuentas.ToListAsync();
        }
    }
}
