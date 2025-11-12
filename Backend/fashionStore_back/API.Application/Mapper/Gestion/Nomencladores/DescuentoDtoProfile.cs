using API.Application.Dtos.Gestion.Nomencladores.Descuento;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class DescuentoDtoProfile : BaseProfile<Descuento, DescuentoDto, CrearDescuentoInputDto, ActualizarDescuentoInputDto, ListadoPaginadoDescuentoDto>

    {
        public DescuentoDtoProfile()
        {
            //   MapDescuentoDto();
            MapDetallesDescuentoDto();
        }


        public void MapDetallesDescuentoDto()
        {
            //CreateMap<Descuento, DetallesDescuentoDto>()
            //  .ReverseMap()
            //;
            // Entity → DTO
            CreateMap<Descuento, DetallesDescuentoDto>()
                .ForMember(dest => dest.ProductosIds,
                    opt => opt.MapFrom(src => src.ProductoDescuentos.Select(pd => pd.ProductoId).ToList()));

            // DTO → Entity
            CreateMap<DetallesDescuentoDto, Descuento>()
                .ForMember(dest => dest.ProductoDescuentos,
                    opt => opt.MapFrom(src => src.ProductosIds.Select(id => new ProductoDescuento
                    {
                        ProductoId = id
                    }).ToList()));
        }

        public override void MapCrearEntityDto()
        {
            // DTO → Entity
            CreateMap<CrearDescuentoInputDto, Descuento>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductoDescuentos, opt => opt.MapFrom(src =>
                    src.ProductosIds.Select(ProductoId => new ProductoDescuento
                    {
                        DescuentoId=src.Id,
                        ProductoId = ProductoId
                    })
                ));

            // Entity → DTO (opcional, si quieres devolver los Ids de productos)
            CreateMap<Descuento, CrearDescuentoInputDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductosIds, opt => opt.MapFrom(src =>
                    src.ProductoDescuentos.Select(pd => pd.ProductoId).ToList()
                ));
        }

        public override void MapActualizarEntityDto()
        {
            CreateMap<ActualizarDescuentoInputDto, Descuento>()
                .ForMember(dest => dest.ProductoDescuentos, opt => opt.Ignore()) // lo manejamos manualmente
                .AfterMap((src, dest) =>
                {
                    // IDs que vienen en el DTO
                    var nuevosIds = src.ProductosIds ?? new List<Guid>();

                    // IDs que ya existen en la entidad
                    var existentesIds = dest.ProductoDescuentos.Select(pd => pd.ProductoId).ToList();

                    // --- Agregar los que faltan ---
                    var paraAgregar = nuevosIds.Except(existentesIds).ToList();
                    foreach (var id in paraAgregar)
                    {
                        dest.ProductoDescuentos.Add(new ProductoDescuento
                        {
                            ProductoId = id,
                            DescuentoId = dest.Id
                        });
                    }

                    // --- Eliminar los que ya no están ---
                    var paraEliminar = dest.ProductoDescuentos
                        .Where(pd => !nuevosIds.Contains(pd.ProductoId))
                        .ToList();

                    foreach (var pd in paraEliminar)
                    {
                        dest.ProductoDescuentos.Remove(pd);
                    }
                });
        }


    }
}
