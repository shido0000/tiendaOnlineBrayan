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
            DateTime fechaHoy = DateTime.Now;

            // Map base Producto -> ProductoDto so AutoMapper can map collections to ProductoDto
            CreateMap<Producto, ProductoDto>()
                .ForMember(dto => dto.CategoriaIds,
                    opt => opt.MapFrom(e => e.ProductoCategorias.Select(pc => pc.CategoriaId).ToList()))
                .ReverseMap();

            CreateMap<Producto, DetallesProductoDto>()
                .IncludeBase<Producto, ProductoDto>()
                .ForMember(dto => dto.CategoriaIds,
                    opt => opt.MapFrom(e => e.ProductoCategorias.Select(pc => pc.CategoriaId).ToList()))
                .ForMember(dto => dto.TieneDescuento,
                    opt => opt.MapFrom(e =>
                        e.ProductoDescuentos.Any(pd =>
                            pd.Descuento.EsActivo &&
                            pd.Descuento.FechaInicio.Date <= fechaHoy.Date &&
                            pd.Descuento.FechaFin.Date >= fechaHoy.Date)))
                // PrecioVentaDescuento lo calculamos en AfterMap porque requiere lógica
                .IncludeBase<Producto, ProductoDto>()
                .AfterMap((src, dest) =>
                {
                    var descuentoActivo = src.ProductoDescuentos
                        .FirstOrDefault(pd =>
                            pd.Descuento.EsActivo &&
                            pd.Descuento.FechaInicio.Date <= fechaHoy.Date &&
                            pd.Descuento.FechaFin.Date >= fechaHoy.Date);

                    if (descuentoActivo != null)
                    {
                        var d = descuentoActivo.Descuento;
                        decimal precioFinal = src.PrecioVenta;

                        if (d.MontoFijo.HasValue && d.MontoFijo.Value > 0)
                        {
                            precioFinal -= d.MontoFijo.Value;
                        }
                        else if (d.Porcentaje.HasValue && d.Porcentaje.Value > 0)
                        {
                            precioFinal -= (precioFinal * d.Porcentaje.Value / 100);
                        }

                        dest.PrecioVentaDescuento = precioFinal < 0 ? 0 : precioFinal;
                    }
                    else
                    {
                        dest.PrecioVentaDescuento = src.PrecioVenta;
                    }
                })
                .ReverseMap();
        }

        public void MapProductoListadoDto()
        {
            DateTime fechaHoy = DateTime.Now;

            CreateMap<Producto, ListadoPaginadoProductoDto>()
                .ForMember(dto => dto.CategoriaIds,
                    opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.CategoriaId).ToList()))
                .ForMember(dto => dto.MonedaCostoCodigo,
                    opt => opt.MapFrom(e => e.MonedaCosto.Codigo ?? "-"))
                .ForMember(dto => dto.MonedaVentaCodigo,
                    opt => opt.MapFrom(e => e.MonedaVenta.Codigo ?? "-"))
                .ForMember(dto => dto.TieneDescuento,
                    opt => opt.MapFrom(e =>
                        e.ProductoDescuentos.Any(pd =>
                            pd.Descuento.EsActivo &&
                            pd.Descuento.FechaInicio.Date <= fechaHoy.Date &&
                            pd.Descuento.FechaFin.Date >= fechaHoy.Date)))
                // PrecioVentaDescuento lo calculamos en AfterMap porque requiere lógica
                .IncludeBase<Producto, ProductoDto>()
                .AfterMap((src, dest) =>
                {
                    var descuentoActivo = src.ProductoDescuentos
                        .FirstOrDefault(pd =>
                            pd.Descuento.EsActivo &&
                            pd.Descuento.FechaInicio.Date <= fechaHoy.Date &&
                            pd.Descuento.FechaFin.Date >= fechaHoy.Date);

                    if (descuentoActivo != null)
                    {
                        var d = descuentoActivo.Descuento;
                        decimal precioFinal = src.PrecioVenta;

                        if (d.MontoFijo.HasValue && d.MontoFijo.Value > 0)
                        {
                            precioFinal -= d.MontoFijo.Value;
                        }
                        else if (d.Porcentaje.HasValue && d.Porcentaje.Value > 0)
                        {
                            precioFinal -= (precioFinal * d.Porcentaje.Value / 100);
                        }

                        dest.PrecioVentaDescuento = precioFinal < 0 ? 0 : precioFinal;
                    }
                    else
                    {
                        dest.PrecioVentaDescuento = src.PrecioVenta;
                    }
                })
                .ReverseMap();
        }
    }
}
