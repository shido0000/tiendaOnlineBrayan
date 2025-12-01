using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class CuponService : BasicService<Cupon, CuponValidator>, ICuponService
    {

        public CuponService(IUnitOfWork<Cupon> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        public async Task<decimal> AplicarCuponAsync(Guid pedidoId, Guid cuponId)
        {
            var pedido = await _repositorios.Pedidos
                        .GetQuery()
                        .Include(p => p.Detalles)
                            .ThenInclude(d => d.ProductoVariante)
                        .FirstOrDefaultAsync(p => p.Id == pedidoId)
                         ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };


            var cupon = await _repositorios.Cupones
                        .GetQuery()
                        .FirstOrDefaultAsync(c => c.Id == cuponId && c.EsActivo)
                        ?? throw new CustomException() { Status = StatusCodes.Status400BadRequest, Message = "Cupón inválido." };

            if (DateTime.UtcNow < cupon.FechaInicio || DateTime.UtcNow > cupon.FechaFin)
                throw new CustomException() { Status = StatusCodes.Status400BadRequest, Message = "Cupón fuera de vigencia." };

            if (cupon.MaximoUsos > 0 && cupon.UsosActuales >= cupon.MaximoUsos)
                throw new CustomException() { Status = StatusCodes.Status400BadRequest, Message = "Cupón agotado." };


            if (cupon.MontoMinimoPedido.HasValue && pedido.Total < cupon.MontoMinimoPedido.Value)
                throw new CustomException() { Status = StatusCodes.Status400BadRequest, Message = "El pedido no cumple el mínimo para aplicar el cupón." };

            // Calcular descuento
            decimal descuento = 0;
            if (cupon.Porcentaje.HasValue)
                descuento = pedido.Total * (cupon.Porcentaje.Value / 100m);
            else if (cupon.MontoFijo.HasValue)
                descuento = cupon.MontoFijo.Value;

            // Aplicar
            pedido.Total -= descuento;
            pedido.CuponId = cupon.Id;
            cupon.UsosActuales++;

            _repositorios.Pedidos.Update(pedido);
            _repositorios.Cupones.Update(cupon);
            await _repositorios.SaveChangesAsync();

            return descuento;
        }

        public async Task<CuponEspecificoDto> ObtenerCuponPorCodigo(string codigo, decimal importePedido)
        {
            var cupon = await _repositorios.Cupones
                            .GetQuery()
                            .AsNoTracking()
                            .FirstOrDefaultAsync(e => e.Codigo.ToLower() == codigo.ToLower())
                            ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Cupón no encontrado." };
            var fechaHoy = DateTime.Today;

            if (cupon.EsActivo)
            {
                if (cupon.FechaInicio.Date <= fechaHoy.Date && fechaHoy <= cupon.FechaFin.Date)
                {
                    if (importePedido > cupon.MontoMinimoPedido)
                    {
                        if (cupon.Porcentaje.HasValue && cupon.Porcentaje.Value > 0)
                        {
                            return new CuponEspecificoDto()
                            {
                                Id = cupon.Id,
                                EsMontoFijo = false,
                                Valor = cupon.Porcentaje.Value,
                            };
                        }
                        else if (cupon.MontoFijo.HasValue && cupon.MontoFijo.Value > 0)
                        {
                            return new CuponEspecificoDto()
                            {
                                Id=cupon.Id,
                                EsMontoFijo = true,
                                Valor = cupon.MontoFijo.Value,
                            };
                        }
                        else throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Cupón no disponible." };
                    }
                    else throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = $"El pedido no supera el monto mínimo para usar: {cupon.MontoMinimoPedido}." };
                }
                else throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Cupón no disponible." };
            }
            else throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Cupón no disponible." };
        }
    }
}