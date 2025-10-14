using API.Application.Dtos.Gestion.Nomencladores.ProductoCategoria;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class ProductoCategoriaDtoProfile : BaseProfile<ProductoCategoria, ProductoCategoriaDto, CrearProductoCategoriaInputDto, ActualizarProductoCategoriaInputDto, ListadoPaginadoProductoCategoriaDto>

    {
        public ProductoCategoriaDtoProfile()
        {
             
           MapProductoCategoriaDto();
        }

        public void MapProductoCategoriaDto()
        {
            CreateMap<ProductoCategoria, DetallesProductoCategoriaDto>()
                 .ReverseMap();
        }

      /*  public void MapDetallesProductoCategoriaDto()
        {
            CreateMap<Venta, DetallesProductoCategoriaDto>()
            .ForMember(dto => dto.Productos, opt => opt.MapFrom(e => e.ProductosProductoCategoriaes.Select(productosProductoCategoriaes => productosProductoCategoriaes.Producto)));
         }*/

       /* public override void MapActualizarEntityDto()
        {
            CreateMap<ProductoCategoria, ActualizarProductoCategoriaInputDto>()
                .ForMember(dto => dto.ProductosIds, opt => opt.MapFrom(e => e.ProductosProductoCategoriaes.Select(e => e.Id)))
                .ForMember(dto => dto.VentasIds, opt => opt.MapFrom(e => e.VentasProductoCategoriaes.Select(e => e.Id)));
            CreateMap<ActualizarProductoCategoriaInputDto, ProductoCategoria>()
                 .ForMember(opt => opt.VentasProductoCategoriaes, dto => dto.MapFrom(e => e.VentasIds.Select(ventaId => new VentaProductoCategoria { VentaId = ventaId, ProductoCategoriaId = e.Id })))
                 .ForMember(opt => opt.ProductosProductoCategoriaes, dto => dto.MapFrom(e => e.ProductosIds.Select(productoId => new ProductoProductoCategoria { ProductoId = productoId, ProductoCategoriaId = e.Id })));
        }

        public override void MapCrearEntityDto()
        {
            CreateMap<ProductoCategoria, CrearProductoCategoriaInputDto>()
                .ForMember(dto => dto.ProductosIds, opt => opt.MapFrom(e => e.ProductosProductoCategoriaes.Select(e => e.Id)))
                .ForMember(dto => dto.VentasIds, opt => opt.MapFrom(e => e.VentasProductoCategoriaes.Select(e => e.Id)));
            CreateMap<CrearProductoCategoriaInputDto, ProductoCategoria>()
                .ForMember(opt => opt.VentasProductoCategoriaes, dto => dto.MapFrom(e => e.VentasIds.Select(ventaId => new VentaProductoCategoria { VentaId = ventaId, ProductoCategoriaId = e.Id })));
        }*/
    }
}
