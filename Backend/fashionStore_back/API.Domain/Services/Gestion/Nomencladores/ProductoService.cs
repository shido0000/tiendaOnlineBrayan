using API.Data.Dto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class ProductoService : BasicService<Producto, ProductoValidator>, IProductoService
    {
        public ProductoService(IUnitOfWork<Producto> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        //public async override Task<EntityEntry<Producto>> Crear(Producto entity)
        //{
        //    // Si el producto trae fotos en memoria (ej. mapeadas desde el DTO)
        //    if (entity.Fotos != null && entity.Fotos.Any())
        //    {
        //        // Aseguramos que cada foto tenga el ProductoId correcto
        //        foreach (var foto in entity.Fotos)
        //        {
        //            foto.ProductoId = entity.Id; // si el Id ya está asignado
        //        }
        //        await _repositorios.ProductosFotos.AddRangeAsync(entity.Fotos);
        //    }

        //    var entry = await base.Crear(entity);
        //    return entry;
        //}

        public async Task<Guid> CrearAsync(CrearProductoInputDto dto, List<IFormFile> fotos)
        {
            var producto = new Producto
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                EsActivo = dto.EsActivo,
                SKU = dto.SKU,
                Talla = dto.Talla,
                Color = dto.Color,
                PrecioCosto = dto.PrecioCosto,
                PrecioVenta = dto.PrecioVenta,
                MonedaCostoId = dto.MonedaCostoId,
                MonedaVentaId = dto.MonedaVentaId,
                Stock = dto.Stock,
            };

            await _repositorios.Productos.AddAsync(producto);
            await _repositorios.SaveChangesAsync(); // aquí ya tienes producto.Id

            // Procesar fotos si vienen en el FormData
            if (fotos != null && fotos.Any())
            {
                for (int i = 0; i < fotos.Count; i++)
                {
                    var file = fotos[i];
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine("wwwroot", "uploads", "productos", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var url = $"/uploads/productos/{fileName}";

                    producto.Fotos.Add(new ProductoFoto
                    {
                        ProductoId = producto.Id,
                        Descripcion = file.FileName,   // 👈 nombre original del archivo
                        Orden = i,                     // 👈 orden según posición en la lista
                        EsPrincipal = (i == 0),        // 👈 solo la primera es principal
                        Url = url
                    });
                }

                await _repositorios.SaveChangesAsync();
            }

            var listaCategorias = new List<ProductoCategoria>();
            if (dto.CategoriaIds.Any())
            {
                foreach (var cateId in dto.CategoriaIds)
                {
                    var nuevoElemento = new ProductoCategoria()
                    {
                        CategoriaId = cateId,
                        ProductoId = producto.Id,
                    };
                    listaCategorias.Add(nuevoElemento);
                }
                await _repositorios.ProductosCategorias.AddRangeAsync(listaCategorias);
                await _repositorios.SaveChangesAsync();
            }

            return producto.Id;
        }

        public async Task<Guid> ActualizarAsync(Guid id, ActualizarProductoInputDto dto, List<IFormFile> fotos)
        {
            var nombresMantener = dto.FotosExistentes
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .ToList();

            var producto = await _repositorios.Productos
                .GetQuery()
                .Include(p => p.Fotos)
                .Include(p => p.ProductoCategorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                throw new Exception("Producto no encontrado");

            // Actualizar propiedades simples
            producto.Codigo = dto.Codigo;
            producto.Descripcion = dto.Descripcion;
            producto.EsActivo = dto.EsActivo;
            producto.SKU = dto.SKU;
            producto.Talla = dto.Talla;
            producto.Color = dto.Color;
            producto.PrecioCosto = dto.PrecioCosto;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.MonedaVentaId = dto.MonedaVentaId;
            producto.MonedaCostoId = dto.MonedaCostoId;
            producto.Stock = dto.Stock;

            // --- Sincronización de fotos ---
            var fotosAEliminar = producto.Fotos
                .Where(f => !nombresMantener.Contains(
                    Path.GetFileNameWithoutExtension(f.Url)))
                .ToList();

            if (fotosAEliminar.Any())
            {
                foreach (var foto in fotosAEliminar)
                {
                    if (!string.IsNullOrEmpty(foto.Url))
                    {
                        var filePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            foto.Url.TrimStart('/')
                        );

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }

                _repositorios.ProductosFotos.RemoveRange(fotosAEliminar);
            }

            // Procesar fotos nuevas
            if (fotos != null && fotos.Any())
            {
                for (int i = 0; i < fotos.Count; i++)
                {
                    var file = fotos[i];
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine("wwwroot", "uploads", "productos", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var url = $"/uploads/productos/{fileName}";

                    producto.Fotos.Add(new ProductoFoto
                    {
                        ProductoId = producto.Id,
                        Descripcion = file.FileName,
                        Orden = i,
                        EsPrincipal = (i == 0),
                        Url = url
                    });
                }
            }

            // --- Sincronización de categorías ---
            var categoriasActuales = producto.ProductoCategorias.Select(c => c.CategoriaId).ToList();
            var categoriasNuevas = dto.CategoriaIds ?? new List<Guid>();

            // 1. Eliminar las que ya no están
            var categoriasAEliminar = producto.ProductoCategorias
                .Where(c => !categoriasNuevas.Contains(c.CategoriaId))
                .ToList();

            if (categoriasAEliminar.Any())
            {
                _repositorios.ProductosCategorias.RemoveRange(categoriasAEliminar);
            }

            // 2. Insertar las nuevas que no existían
            var categoriasAInsertar = categoriasNuevas
                .Where(cateId => !categoriasActuales.Contains(cateId))
                .Select(cateId => new ProductoCategoria
                {
                    CategoriaId = cateId,
                    ProductoId = producto.Id
                })
                .ToList();

            if (categoriasAInsertar.Any())
            {
                await _repositorios.ProductosCategorias.AddRangeAsync(categoriasAInsertar);
            }

            // Solo Update del producto, EF ya trackea las fotos nuevas
            _repositorios.Productos.Update(producto);

            await _repositorios.SaveChangesAsync();
            return producto.Id;
        }

        public async Task<List<Producto>> ObtenerProductosNovedades()
        {
            //var lista = await _repositorios.Productos
            //                .GetQuery()
            //                .AsNoTracking()
            //                .Include(e => e.Fotos)
            //                .Include(e => e.ProductoCategorias)
            //                    .ThenInclude(e => e.Categoria)
            //                .ToListAsync();

            //var hoy = DateTime.Today; // solo la fecha, sin hora
            //var desde = hoy.AddDays(-5);

            //lista = lista.Where(e => e.FechaCreado.Date >= desde && e.FechaCreado.Date <= hoy).ToList();

            //return lista;

            return new List<Producto>();
        }

        public async Task<List<ProductosAgrupados>> ObtenerProductosAgrupados()
        {
            var productos = await _repositorios.Productos
                .GetQuery()
                .AsNoTracking()
                .Include(p => p.ProductoCategorias)
                .Include(p => p.Fotos)
                .ToListAsync();

            var agrupados = productos
                .Where(p => !string.IsNullOrEmpty(p.SKU) && p.SKU.Length >= 2)
                .GroupBy(p => p.SKU.Substring(0, 2)) // 👈 agrupar por los 2 primeros caracteres
                .Select(g => new ProductosAgrupados
                {
                    SKU = g.Key, // el prefijo de 2 caracteres
                    Codigo = g.First().Codigo,
                    Descripcion = g.First().Descripcion,
                    EsActivo = g.First().EsActivo,
                    PrecioVenta = g.First().PrecioVenta,
                    MonedaVenta = g.First().MonedaVenta?.Descripcion, // si MonedaVenta tiene campo Descripcion
                    Stock = g.Sum(p => p.Stock), // sumar stock de todos los productos del grupo
                    Talla = g.Select(p => p.Talla).Where(t => !string.IsNullOrEmpty(t)).Distinct().ToList(),
                    Color = g.Select(p => p.Color).Where(c => !string.IsNullOrEmpty(c)).Distinct().ToList(),
                    CategoriaIds = g.SelectMany(p => p.ProductoCategorias.Select(pc => pc.CategoriaId)).Distinct().ToList(),
                    // si quieres traer fotos, aquí deberías mapearlas a IFormFile o a otra estructura
                    Fotos = new List<IFormFile>() // placeholder, depende de cómo quieras mapear ProductoFoto
                })
                .ToList();

            return agrupados;
        }

        public async Task<ProductosAgrupados?> ObtenerProductoAgrupadoPorSku(string sku)
        {
            if (string.IsNullOrEmpty(sku) || sku.Length < 2)
                return null;

            var prefijo = sku.Substring(0, 2);

            var productos = await _repositorios.Productos
                .GetQuery()
                .AsNoTracking()
                .Include(p => p.ProductoCategorias)
                .Include(p => p.Fotos)
                .Where(p => p.SKU != null && p.SKU.StartsWith(prefijo)) // 👈 filtrar por prefijo
                .ToListAsync();

            if (!productos.Any())
                return null;

            var agrupado = new ProductosAgrupados
            {
                SKU = prefijo,
                Codigo = productos.First().Codigo,
                Descripcion = productos.First().Descripcion,
                EsActivo = productos.First().EsActivo,
                PrecioVenta = productos.First().PrecioVenta,
                MonedaVenta = productos.First().MonedaVenta?.Descripcion,
                Stock = productos.Sum(p => p.Stock),
                Talla = productos.Select(p => p.Talla)
                                 .Where(t => !string.IsNullOrEmpty(t))
                                 .Distinct()
                                 .ToList(),
                Color = productos.Select(p => p.Color)
                                 .Where(c => !string.IsNullOrEmpty(c))
                                 .Distinct()
                                 .ToList(),
                CategoriaIds = productos.SelectMany(p => p.ProductoCategorias
                                                          .Select(pc => pc.CategoriaId))
                                        .Distinct()
                                        .ToList(),
                Fotos = new List<IFormFile>() // aquí deberías mapear tus ProductoFoto si lo necesitas
            };

            return agrupado;
        }

    }
}