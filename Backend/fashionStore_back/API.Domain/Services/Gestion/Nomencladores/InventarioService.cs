using API.Data.Dto.VisualProductosPorCategoria;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Domain.Services.Gestion.Nomencladores
{
    public class InventarioService : BasicService<Inventario, InventarioValidator>, IInventarioService
    {

        public InventarioService(IUnitOfWork<Inventario> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        public async override Task ValidarAntesActualizar(Inventario entity)
        {
            Inventario? inventario = await ObtenerPorId(entity.Id) ??
               throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Elemento no encontrado." };

            if (inventario.CantidadReservada == inventario.CantidadDisponible && entity.CantidadDisponible < inventario.CantidadDisponible)
            {

                throw new CustomException() { Status = StatusCodes.Status400BadRequest, Message = "Tiene ese producto reservado no puede rebajarlo." };
            }

            await base.ValidarAntesActualizar(entity);
        }

        public async override Task ValidarAntesEliminar(Guid id)
        {
            Inventario? inventario = await ObtenerPorId(id) ??
            throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Elemento no encontrado." };

            if (inventario.CantidadReservada != 0)
            {

                throw new CustomException() { Status = StatusCodes.Status400BadRequest, Message = "Tiene ese producto reservado no puede eliminarlo." };
            }
            await base.ValidarAntesEliminar(id);
        }


        public async override Task<EntityEntry<Inventario>> Actualizar(Inventario entity)
        {
            Inventario? inventario = await ObtenerPorId(entity.Id) ??
               throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Elemento no encontrado." };

            inventario.CantidadDisponible = entity.CantidadDisponible;

            return await base.Actualizar(inventario);
        }

        //public async Task<List<ProductoPorCategoriaDto>> ObtenerProductosDelInventarioPorCategoria(Guid categoriaId)
        //{
        //    var listadoRetorno = new List<ProductoPorCategoriaDto>();
        //    var inventarios = await _repositorios.Inventarios
        //                        .GetQuery()
        //                        .AsNoTracking()
        //                        .Include(e => e.Producto)
        //                            .ThenInclude(e => e.ProductoCategorias)
        //                        .Include(e => e.Producto)
        //                            .ThenInclude(e => e.Moneda)
        //                        .Include(e => e.Producto)
        //                            .ThenInclude(e => e.Fotos)
        //                        .Where(e => e.Producto.ProductoCategorias.Any(e => e.CategoriaId == categoriaId))
        //                        .ToListAsync();

        //    foreach (var inv in inventarios)
        //    {
        //        var nuevoElemento = new ProductoPorCategoriaDto()
        //        {
        //            ProductoId = inv.ProductoId,
        //            CantidadDisponible = inv.CantidadReservada-inv.CantidadDisponible,
        //            Estado = (inv.CantidadReservada - inv.CantidadDisponible) == 0 ? EstadoProductoInventario.Agotado.ToString() : EstadoProductoInventario.Disponible.ToString(),
        //            Codigo = inv.Producto.Codigo,
        //            Descripcion = inv.Producto.Descripcion,
        //            SKU = inv.Producto.SKU,
        //            Color = inv.Producto.Color,
        //            Moneda = inv.Producto.Moneda.Codigo ?? "",
        //            PrecioVenta = inv.Producto.PrecioVenta,
        //            Fotos = inv.Producto.Fotos.Select(f => f.Url).ToList(), // 👈 aquí el fix
        //            CategoriaIds = inv.Producto.ProductoCategorias.Select(pc => pc.CategoriaId).ToList()
        //        };
        //        listadoRetorno.Add(nuevoElemento);
        //    }

        //    return listadoRetorno;
        //}
    }
}