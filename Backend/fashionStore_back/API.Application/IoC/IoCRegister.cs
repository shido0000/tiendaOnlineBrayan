﻿using API.Application.Mapper;
using API.Data.DbContexts;
using API.Data.Entidades;
using API.Data.IUnitOfWorks;
using API.Data.IUnitOfWorks.Interfaces;
using API.Data.IUnitOfWorks.Repositorios;
using API.Domain.Interfaces;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Interfaces.Inicio;
using API.Domain.Interfaces.Seguridad;
using API.Domain.Services;
using API.Domain.Services.Contabilidad;
using API.Domain.Services.Gestion.Nomencladores;
using API.Domain.Services.Inicio;
using API.Domain.Services.Seguridad;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace API.Application.IoC
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegistrarDataContext(configuration);
            services.RegistrarAutenticacion(configuration);
            services.RegistrarServicios();
            services.RegistrarRepositorios();
            services.RegistrarServiciosDominio();
            services.RegistrarSwagger();
            services.RegistrarHangfire();

            return services;
        }


        public static IServiceCollection RegistrarServicios(this IServiceCollection services)
        {

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                //evita que se validen automaticamente los Dto mostrando un formato de error distinto
                options.SuppressModelStateInvalidFilter = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddAutoMappers(AutoMapperConfiguration.CreateExpression().AddAutoMapperLeadOportunidade());

            services.AddHttpContextAccessor();

            services.AddCors();


            //Add services to validation

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<Program>();

            // Ignore bucles in json
            services.AddMvc()
                    .AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                    .AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            return services;
        }

        public static void RegistrarHangfire(this IServiceCollection services)
        {
            services.AddHangfire(configuration =>
            {
                IGlobalConfiguration<AutomaticRetryAttribute> globalConfiguration = configuration
                                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                    .UseSimpleAssemblyNameTypeSerializer()
                                    .UseRecommendedSerializerSettings(settings => settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                                    .UseFilter(new AutomaticRetryAttribute { Attempts = 2 });
                globalConfiguration
                .UseInMemoryStorage();
            });

            services.AddHangfireServer();
        }

        private static void RegistrarSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API del Sistema",
                    Description = "Contiene las funcionalidades necesarias para el funcionamiento del sistema"
                });
                //permite insertar el token por el SaggerUI
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT cabecera de Authorizacion usando el esquema Bearer. 
                                    Inserte 'Bearer' [espacio] y luego el token en el campo de texto de abajo.
                                    Ejemplo: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluLnN5c3RlbSIsImp0aSI6IjI1NWYyNDU4LTliMjktNDgwZC1iMWY5LWQ3OWRkODhkOTA2NSIsImdlc3Rpb25hciB1c3VhcmlvcyI6Imdlc3Rpb25hciB1c3VhcmlvcyIsImxpc3RhciByb2xlcyI6Imxpc3RhciByb2xlcyIsImxpc3RhciB1c3VhcmlvcyI6Imxpc3RhciB1c3VhcmlvcyIsImdlc3Rpb25hciByb2wiOiJnZXN0aW9uYXIgcm9sIiwiZXhwIjoxOTU5NzQ4NDQxLCJpc3MiOiJBcGlTeXN0ZW0iLCJhdWQiOiJBcGlTeXN0ZW0ifQ.POxB0z7od3VhU0lYAfY6X0_6ruQtDwrRUwncqUBCZ7A'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
                //using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static IApplicationBuilder AddRegistration(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(e => e
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            );

            app.UseAuthorization();

            app.MapControllers();

            app.UseHangfireDashboard();

            app.UseStaticFiles();


            app.Run();

            return app;
        }

        public static IServiceCollection RegistrarDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            string databaseType = configuration["DatabaseType"] ?? "MSSqlServer";

            if (databaseType == "MSSqlServer")
            {
                services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("APIContext"), sqlOptions => sqlOptions.CommandTimeout(1300)));
                services.AddDbContext<TrazasDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TrazasContext")));
            }
            
            services.AddTransient<IApiDbContext, ApiDbContext>();
            services.AddTransient<ITrazasDbContext, TrazasDbContext>();

            //Aplica las migraciones pendientes. Crea la base datos si no existe.     
            services.BuildServiceProvider().GetRequiredService<ApiDbContext>().Database.Migrate();
            services.BuildServiceProvider().GetRequiredService<TrazasDbContext>().Database.Migrate();

            return services;
        }

        public static IServiceCollection RegistrarRepositorios(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }

        public static IServiceCollection RegistrarServiciosDominio(this IServiceCollection services)
        {
            // SEGURIDAD
            services.AddScoped<IAutenticacionService, AutenticacionService>();
            services.AddScoped<IPermisoService, PermisoService>();
            services.AddScoped<IRolPermisoService, RolPermisoService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();


            // GESTION
            services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();
            services.AddScoped<IDescuentoService, DescuentoService>();
            services.AddScoped<IInventarioService, InventarioService>();
            services.AddScoped<IMonedaService, MonedaService>();
            services.AddScoped<IPedidoDetalleService, PedidoDetalleService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IProductoCategoriaService, ProductoCategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaDetalleService, VentaDetalleService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<ICuponService, CuponService>();
            services.AddScoped<IListaDeseosDetalleService, ListaDeseosDetalleService>();
            services.AddScoped<IListaDeseosService, ListaDeseosService>();
            services.AddScoped<ICarritoService, CarritoService>();
            services.AddScoped<ICarritoDetalleService, CarritoDetalleService>();
            services.AddScoped<IComprobanteVentaService, ComprobanteVentaService>();
            services.AddScoped<IDatosInicioService, DatosInicioService>();


            // CONTABILIDAD
            services.AddScoped<IAsientoContableService, AsientoContableService>();
            services.AddScoped<ICuentaContableService, CuentaContableService>();
            services.AddScoped<IMovimientoContableService, MovimientoContableService>();

            // BASE
            services.AddScoped(typeof(IBaseService<EntidadBase, AbstractValidator<EntidadBase>>), typeof(BasicService<EntidadBase, AbstractValidator<EntidadBase>>));

            return services;
        }
        private static void RegistrarAutenticacion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                // Configure JWT Bearer Auth to expect our security key
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["ValidationParameters:Issuer"],
                    ValidAudience = configuration["ValidationParameters:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"] ?? "APSKP3KP4234KP2423K4P234K2P34K23P4K234K23423K42P3")),
                    ClockSkew = TimeSpan.Zero
                };
                // Authentication Personalizada
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        //Validando la fecha de expiracion del token
                        string tokenExpiration = context.Request.Headers["tokenExpiration"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(tokenExpiration) && DateTime.Parse(tokenExpiration) < DateTime.Now)
                        {
                            context.Fail(new SecurityTokenExpiredException("La sesion ha expirado."));
                            return Task.CompletedTask;
                        }

                        return Task.CompletedTask;
                    }
                };

            });

        }

        internal static void AddLogsRegistration(WebApplicationBuilder builder)
        {
            IConfiguration serilogConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(serilogConfiguration)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}