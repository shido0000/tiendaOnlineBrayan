using API.Application.Mapper.Contabilidad;
using API.Application.Mapper.Gestion.Nomencladores;
using API.Application.Mapper.Seguridad;
using AutoMapper;

namespace API.Application.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMappers(this IServiceCollection services, MapperConfigurationExpression cfg)
        {
            IMapper mapper = new MapperConfiguration(cfg).CreateMapper();
            services.AddSingleton(mapper);
        }
        public static MapperConfigurationExpression AddAutoMapperLeadOportunidade(this MapperConfigurationExpression cfg)
        {
            cfg.AddProfile<UsuarioDtoProfile>();
            cfg.AddProfile<RolDtoProfile>();
            cfg.AddProfile<PermisoDtoProfile>();

            // GESTION
            cfg.AddProfile<CategoriaProductoDtoProfile>();
            cfg.AddProfile<DescuentoDtoProfile>();
            cfg.AddProfile<InventarioDtoProfile>();
            cfg.AddProfile<MonedaDtoProfile>();
            cfg.AddProfile<PedidoDetalleDtoProfile>();
            cfg.AddProfile<PedidoDtoProfile>();
            cfg.AddProfile<ProductoCategoriaDtoProfile>();
            cfg.AddProfile<ProductoDtoProfile>();
            cfg.AddProfile<VentaDetalleDtoProfile>();
            cfg.AddProfile<VentaDtoProfile>();
            cfg.AddProfile<CuponDtoProfile>();
            cfg.AddProfile<ListaDeseosDtoProfile>();
            cfg.AddProfile<ListaDeseosDetalleDtoProfile>();
            cfg.AddProfile<CarritoDtoProfile>();
            cfg.AddProfile<CarritoDetalleDtoProfile>();
            cfg.AddProfile<MensajeriaDtoProfile>();
            cfg.AddProfile<GestorDtoProfile>();

            // CONTABILIDAD
            cfg.AddProfile<CuentaContableDtoProfile>();
            cfg.AddProfile<AsientoContableDtoProfile>();
            cfg.AddProfile<MovimientoContableDtoProfile>();

            return cfg;
        }
        public static MapperConfigurationExpression CreateExpression()
        {
            return new MapperConfigurationExpression();
        }
    }
}
