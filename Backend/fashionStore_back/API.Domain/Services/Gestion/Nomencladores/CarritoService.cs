using API.Data.Dto.Carrito;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class CarritoService : BasicService<Carrito, CarritoValidator>, ICarritoService
    {

        public CarritoService(IUnitOfWork<Carrito> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        public async Task<Guid> EnviarDatosAlCarrito(DatosCarritoDto datosCarritoDto)
        {
            var listaCarritoDetallesNuevo = new List<CarritoDetalle>();

            var carritosExistentes = await _repositorios.Carritos
                                .GetQuery()
                                .AsTracking()
                                .Where(e => e.UsuarioId == datosCarritoDto.UsuarioId)
                                .ToListAsync();
            _repositorios.Carritos.RemoveRange(carritosExistentes);

            var carritoNuevo = new Carrito
            {
                Id = Guid.NewGuid(),
                UsuarioId = datosCarritoDto.UsuarioId,
                FechaCreacion = datosCarritoDto.FechaCreacion,
                Detalles = new List<CarritoDetalle>()
            };

            var nombreCliente = await _repositorios.Usuarios
                                    .GetQuery()
                                    .AsNoTracking()
                                    .Where(e => e.Id == datosCarritoDto.UsuarioId)
                                    .Select(e => e.NombreCompleto)
                                    .FirstOrDefaultAsync() ?? "";

            await _repositorios.Carritos.AddAsync(carritoNuevo);

            foreach (var detalles in datosCarritoDto.Detalles)
            {
                var nuevoDetalle = new CarritoDetalle
                {
                    Id = Guid.NewGuid(),
                    CarritoId = carritoNuevo.Id,
                    Cantidad = detalles.Cantidad,
                    ProductoId = detalles.ProductoId,
                    UnitPrice = detalles.UnitPrice,
                    LineTotal = detalles.LineTotal,
                    FechaCreado = DateTime.Now,
                    FechaActualizado = DateTime.Now,
                    CreadoPor = nombreCliente,
                    ActualizadoPor = nombreCliente,
                };
                listaCarritoDetallesNuevo.Add(nuevoDetalle);
            }
            await _repositorios.CarritosDetalles.AddRangeAsync(listaCarritoDetallesNuevo);
            await _repositorios.SaveChangesAsync();

            return carritoNuevo.Id;
        }

        public async Task<List<DatosObtenerCarritoDto>> ObtenerDatosCarritoReal()
        {
            var listaCarritoRetorno = new List<DatosObtenerCarritoDto>();

            var carritos = await _repositorios.Carritos
                                 .GetQuery()
                                 .AsNoTracking()
                                 .Include(e => e.Usuario)
                                 .Include(e => e.Detalles)
                                 .ThenInclude(e => e.Producto)
                                 .ToListAsync();

            foreach (var car in carritos)
            {
                var nuevoCarrito = new DatosObtenerCarritoDto
                {
                    FechaCreacion = car.FechaCreacion,
                    UsuarioId = car.UsuarioId,
                    NombreCliente = car.Usuario.Nombre,
                    ApellidoCliente = car.Usuario.Apellidos,
                    EmailCliente = car.Usuario.Correo,
                    TelefonoCliente = car.Usuario.Telefono,
                    Detalles = car.Detalles.Select(e => new DatosObtenerCarritoDetalleDto
                    {
                        Cantidad = e.Cantidad,
                        CarritoId = e.CarritoId,
                        CodigoProducto = e.Producto?.Codigo ?? "",
                        DescripcionProducto = e.Producto?.Descripcion ?? "",
                        LineTotal = e.LineTotal,
                        UnitPrice = e.UnitPrice,
                        ProductoId = e.ProductoId,
                    }).ToList()
                };
                listaCarritoRetorno.Add(nuevoCarrito);
            }

            return listaCarritoRetorno;
        }

    }
}