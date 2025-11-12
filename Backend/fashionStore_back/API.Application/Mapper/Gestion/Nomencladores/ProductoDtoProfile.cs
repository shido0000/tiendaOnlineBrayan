using API.Application.Dtos.Gestion.Nomencladores.Producto;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class ProductoDtoProfile : BaseProfile<Producto, DetallesProductoDto, CrearProductoInputDto, ActualizarProductoInputDto, ListadoPaginadoProductoDto>

    {
        public ProductoDtoProfile()
        {
            MapProductoDto();
            MapProductoListadoDto();
        }

        public void MapProductoDto()
        {
            CreateMap<Producto, DetallesProductoDto>()
                .ForMember(dto => dto.CategoriaIds,
                    opt => opt.MapFrom(e => e.ProductoCategorias.Select(pc => pc.CategoriaId).ToList()))
                //.ForMember(dto => dto.FotosExistentes,
                //    opt => opt.MapFrom(v => v.Fotos.Select(f => f.Url).ToList()))
                .ReverseMap()
                .ReverseMap();
        }

        public void MapProductoListadoDto()
        {
            // Producto → ListadoPaginadoProductoDto (hereda de ProductoDto)
            CreateMap<Producto, ListadoPaginadoProductoDto>()
                .ForMember(dto => dto.CategoriaIds, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.CategoriaId).ToList()))
                //.ForMember(dto => dto.FotosExistentes,
                //    opt => opt.MapFrom(v => v.Fotos.Select(f => f.Url).ToList()))
                .ReverseMap();
        }


    }
}
