using API.Data.Dto;
using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Domain.Services.Gestion.Nomencladores
{
    public class DescuentoService : BasicService<Descuento, DescuentoValidator>, IDescuentoService
    {

        public DescuentoService(IUnitOfWork<Descuento> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        public async Task<DatosDescuento?> ObtenerDescuentoActivoDelProducto(Guid productoId)
        {
            var fechaHoy = DateTime.Now.Date;

            // Traer solo los IDs de descuentos asociados al producto
            var productosDescuentosIds = await _repositorios.ProductosDescuentos
                .GetQuery()
                .AsNoTracking()
                .Where(e => e.ProductoId == productoId)
                .Select(e => e.DescuentoId)
                .ToListAsync();

            // Buscar el primer descuento vigente
            var descuentoActivo = await _repositorios.Descuentos
                .GetQuery()
                .AsNoTracking()
                .Where(e =>
                    productosDescuentosIds.Contains(e.Id) &&
                    e.FechaInicio.Date <= fechaHoy &&
                    fechaHoy <= e.FechaFin.Date && e.EsActivo)
                .Select(e => new DatosDescuento
                {
                    DescuentoId = e.Id,
                    Valor = (e.Porcentaje.HasValue && e.Porcentaje.Value != 0) ? e.Porcentaje.Value : e.MontoFijo.Value,
                    EsMontoFijo = (e.Porcentaje.HasValue && e.Porcentaje.Value != 0) ? false : true,
                })
                .FirstOrDefaultAsync();

            // Si no hay descuento, devolver null
            return descuentoActivo;
        }



        //public async Task<decimal> CalcularDescuentoAsync(Guid productoId, int cantidad, bool acumulativo, bool aplicarMayor)
        //{
        //    var producto = await _repositorios.Productos.GetQuery().FirstOrDefaultAsync(e => e.Id == productoId)
        //                   ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Producto no encontrado." };

        //    var subtotal = producto.PrecioVenta * cantidad;

        //    var descuentos = await _repositorios.Descuentos
        //        .GetQuery()
        //        .Where(d => d.ProductoId == productoId
        //                 && d.EsActivo
        //                 && DateTime.UtcNow >= d.FechaInicio
        //                 && DateTime.UtcNow <= d.FechaFin)
        //        .ToListAsync();

        //    if (!descuentos.Any())
        //        return 0;

        //    var valores = descuentos.Select(d =>
        //    {
        //        if (d.Porcentaje.HasValue)
        //            return subtotal * (d.Porcentaje.Value / 100m);
        //        if (d.MontoFijo.HasValue)
        //            return d.MontoFijo.Value;
        //        return 0;
        //    }).ToList();

        //    if (acumulativo)
        //        return valores.Sum();

        //    if (aplicarMayor)
        //        return valores.Max();

        //    return valores.First(); // por defecto
        //}

        public async Task<ResultadoDescuento?> CalcularDescuentoAsync(
    Guid productoId, int cantidad, bool acumulativo, bool aplicarMayor)
        {
            var producto = await _repositorios.Productos
                .GetQuery()
                .FirstOrDefaultAsync(e => e.Id == productoId)
                ?? throw new CustomException
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Producto no encontrado."
                };

            var subtotal = producto.PrecioVenta * cantidad;
            var ahora = DateTime.UtcNow;

            var descuentos = await _repositorios.Descuentos
                .GetQuery()
                .Where(d => d.EsActivo
                         && d.FechaInicio <= ahora
                         && d.FechaFin >= ahora
                         && d.ProductoDescuentos.Any(pd => pd.ProductoId == productoId))
                .ToListAsync();

            if (!descuentos.Any())
                return null;

            // Calcular lista de resultados
            var resultados = descuentos.Select(d =>
            {
                decimal valor = 0;
                string tipo = string.Empty;

                if (d.Porcentaje.HasValue && d.Porcentaje.Value > 0)
                {
                    valor = subtotal * (d.Porcentaje.Value / 100m);
                    tipo = "Porcentaje";
                }
                else if (d.MontoFijo.HasValue && d.MontoFijo.Value > 0)
                {
                    valor = d.MontoFijo.Value;
                    tipo = "MontoFijo";
                }

                return new ResultadoDescuento
                {
                    ValorAplicado = valor,
                    Nombre = d.Nombre,
                    Tipo = tipo,
                    Porcentaje = d.Porcentaje,
                    MontoFijo = d.MontoFijo
                };
            })
            .Where(r => r.ValorAplicado > 0)
            .ToList();

            if (!resultados.Any())
                return null;

            if (acumulativo)
            {
                return new ResultadoDescuento
                {
                    ValorAplicado = resultados.Sum(r => r.ValorAplicado),
                    Nombre = string.Join(" + ", resultados.Select(r => r.Nombre)),
                    Tipo = "Acumulativo"
                };
            }

            if (aplicarMayor)
            {
                return resultados.OrderByDescending(r => r.ValorAplicado).First();
            }

            // Por defecto: el primero (puedes ordenar por fecha de inicio si quieres)
            return resultados.First();
        }

        public override Task<EntityEntry<Descuento>> Crear(Descuento entity)
        {
            return base.Crear(entity);
        }

        public override async Task<EntityEntry<Descuento>> Actualizar(Descuento entity)
        {
            // Traer el descuento actual con sus relaciones
            var descuentoExistente = await _repositorios.Descuentos
                .GetQuery()
                .Include(d => d.ProductoDescuentos)
                .FirstOrDefaultAsync(d => d.Id == entity.Id);

            if (descuentoExistente == null)
                throw new CustomException { Message = "Descuento no encontrado" };

            // Actualizar propiedades simples
            descuentoExistente.Nombre = entity.Nombre;
            descuentoExistente.Porcentaje = entity.Porcentaje;
            descuentoExistente.MontoFijo = entity.MontoFijo;
            descuentoExistente.FechaInicio = entity.FechaInicio;
            descuentoExistente.FechaFin = entity.FechaFin;
            descuentoExistente.EsActivo = entity.EsActivo;

            // --- Sincronizar productos ---
            var nuevosIds = entity.ProductoDescuentos.Select(pd => pd.ProductoId).ToList();
            var existentesIds = descuentoExistente.ProductoDescuentos.Select(pd => pd.ProductoId).ToList();

            // Agregar los que faltan
            var paraAgregar = nuevosIds.Except(existentesIds).ToList();
            foreach (var id in paraAgregar)
            {
                descuentoExistente.ProductoDescuentos.Add(new ProductoDescuento
                {
                    ProductoId = id,
                    DescuentoId = descuentoExistente.Id
                });
            }

            // Eliminar los que ya no están
            var paraEliminar = descuentoExistente.ProductoDescuentos
                .Where(pd => !nuevosIds.Contains(pd.ProductoId))
                .ToList();

            foreach (var pd in paraEliminar)
            {
                descuentoExistente.ProductoDescuentos.Remove(pd);
            }

            _repositorios.ProductosDescuentos.RemoveRange(paraEliminar);

            //return _repositorios.Entry(descuentoExistente);
            return await base.Actualizar(descuentoExistente);
        }

    }
}