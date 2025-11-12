using API.Application.Dtos.Gestion.Nomencladores.Inventario;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class InventarioDtoProfile : BaseProfile<Inventario, InventarioDto, CrearInventarioInputDto, ActualizarInventarioInputDto, ListadoPaginadoInventarioDto>

    {
        public InventarioDtoProfile()
        {
            MapDetallesInventarioDto();
            MapListadoInventarioDto();
        }


        public void MapDetallesInventarioDto()
        {
            CreateMap<Inventario, DetallesInventarioDto>()
                //.ForMember(dto => dto.ProductoCodigo, opt => opt.MapFrom(e => e.Producto.Codigo))
                //.ForMember(dto => dto.ProductoDescripcion, opt => opt.MapFrom(e => e.Producto.Descripcion))
                //.ForMember(dto => dto.ProductoSku, opt => opt.MapFrom(e => e.Producto.SKU))
                //.ForMember(dto => dto.EstadoProductoInventario, opt => opt.MapFrom(e => e.EstadoProductoInventario.ToString()))
                .ReverseMap();
        }
        public void MapListadoInventarioDto()
        {
            CreateMap<Inventario, ListadoPaginadoInventarioDto>()
                //.ForMember(dto => dto.ProductoCodigo, opt => opt.MapFrom(e => e.Producto.Codigo))
                //.ForMember(dto => dto.ProductoDescripcion, opt => opt.MapFrom(e => e.Producto.Descripcion))
                //.ForMember(dto => dto.ProductoSku, opt => opt.MapFrom(e => e.Producto.SKU))
                //.ForMember(dto => dto.EstadoProductoInventario, opt => opt.MapFrom(e => e.EstadoProductoInventario.ToString()))
                .ReverseMap();
        }
    }
}
