﻿using API.Data.Entidades.Gestion.Nomencladores;
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
                            .ThenInclude(d => d.Producto)
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
    }
}