using API.Data.Dto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


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

        //public async Task<Guid> CrearAsync(CrearProductoInputDto dto, List<IFormFile> fotos)
        //{
        //    var producto = new Producto
        //    {
        //        Codigo = dto.Codigo,
        //        Descripcion = dto.Descripcion,
        //        Color = dto.Color,
        //        SKU = dto.SKU,
        //        PrecioCosto = dto.PrecioCosto,
        //        PrecioVenta = dto.PrecioVenta,
        //        MonedaId = dto.MonedaId,
        //        EsActivo = dto.EsActivo
        //    };

        //    await _repositorios.Productos.AddAsync(producto);
        //    await _repositorios.SaveChangesAsync(); // aquí ya tienes producto.Id

        //    // Procesar fotos si vienen en el FormData
        //    if (fotos != null && fotos.Any())
        //    {
        //        for (int i = 0; i < fotos.Count; i++)
        //        {
        //            var file = fotos[i];
        //            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //            var path = Path.Combine("wwwroot", "uploads", "productos", fileName);

        //            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }

        //            var url = $"/uploads/productos/{fileName}";

        //            producto.Fotos.Add(new ProductoFoto
        //            {
        //                ProductoId = producto.Id,
        //                Descripcion = file.FileName,   // 👈 nombre original del archivo
        //                Orden = i,                     // 👈 orden según posición en la lista
        //                EsPrincipal = (i == 0),        // 👈 solo la primera es principal
        //                Url = url
        //            });
        //        }

        //        await _repositorios.SaveChangesAsync();
        //    }

        //    var listaCategorias = new List<ProductoCategoria>();
        //    if (dto.CategoriaIds.Any())
        //    {
        //        foreach (var cateId in dto.CategoriaIds)
        //        {
        //            var nuevoElemento = new ProductoCategoria()
        //            {
        //                CategoriaId = cateId,
        //                ProductoId = producto.Id,
        //            };
        //            listaCategorias.Add(nuevoElemento);
        //        }
        //        await _repositorios.ProductosCategorias.AddRangeAsync(listaCategorias);
        //        await _repositorios.SaveChangesAsync();
        //    }

        //    return producto.Id;
        //}

        public async Task<Guid> CrearAsync(CrearProductoInputDto dto)
        {
            // Crear el producto base
            var producto = new Producto
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                EsActivo = dto.EsActivo
            };

            await _repositorios.Productos.AddAsync(producto);
            await _repositorios.SaveChangesAsync(); // aquí ya tienes producto.Id

            var listadoVariantes = new List<EntityEntry>();
            // Crear variante principal (puedes extender para múltiples variantes)
            foreach (var element in dto.Variants)
            {
                var variante = new ProductVariant
                {
                    ProductoId = producto.Id,
                    SKU = element.SKU,
                    Color = element.Color,
                    Talla = element.Talla,
                    PrecioCosto = element.PrecioCosto,
                    PrecioVenta = element.PrecioVenta,
                    MonedaCostoId = element.MonedaCostoId,
                    MonedaVentaId = element.MonedaVentaId,
                    Stock = element.Stock
                };

                var result = await _repositorios.ProductVariants.AddAsync(variante);

                // Procesar fotos si vienen en el FormData
                if (element.Fotos != null && element.Fotos.Any())
                {
                    var fotosVariante = new List<ProductoFoto>();

                    for (int i = 0; i < element.Fotos.Count; i++)
                    {
                        var file = element.Fotos[i];
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var path = Path.Combine("wwwroot", "uploads", "productos", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var url = $"/uploads/productos/{fileName}";

                        fotosVariante.Add(new ProductoFoto
                        {
                            ProductVariantId = result.Entity.Id,
                            Descripcion = file.FileName,   // nombre original del archivo
                            Orden = i,                     // orden según posición en la lista
                            EsPrincipal = (i == 0),        // solo la primera es principal
                            Url = url
                        });
                    }

                    await _repositorios.ProductosFotos.AddRangeAsync(fotosVariante);
                }
            }
             
            // Asociar categorías
            if (dto.CategoriaIds != null && dto.CategoriaIds.Any())
            {
                var listaCategorias = dto.CategoriaIds.Select(cateId => new ProductoCategoria
                {
                    CategoriaId = cateId,
                    ProductoId = producto.Id
                }).ToList();

                await _repositorios.ProductosCategorias.AddRangeAsync(listaCategorias);
            }

            await _repositorios.SaveChangesAsync();

            return producto.Id;
        }

        public async Task<Guid> ActualizarAsync(Guid id, ActualizarProductoInputDto dto)
        {
            var producto = await _repositorios.Productos
                .GetQuery()
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Fotos)
                .Include(p => p.ProductoCategorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                throw new Exception("Producto no encontrado");

            // --- Actualizar propiedades simples del producto ---
            producto.Codigo = dto.Codigo;
            producto.Descripcion = dto.Descripcion;
            producto.EsActivo = dto.EsActivo;

            // --- Actualizar categorías ---
            var categoriasActuales = producto.ProductoCategorias.Select(c => c.CategoriaId).ToList();
            var categoriasNuevas = dto.CategoriaIds ?? new List<Guid>();

            // Eliminar las que ya no están
            var categoriasAEliminar = producto.ProductoCategorias
                .Where(c => !categoriasNuevas.Contains(c.CategoriaId))
                .ToList();

            if (categoriasAEliminar.Any())
                _repositorios.ProductosCategorias.RemoveRange(categoriasAEliminar);

            // Insertar las nuevas
            var categoriasAInsertar = categoriasNuevas
                .Where(cateId => !categoriasActuales.Contains(cateId))
                .Select(cateId => new ProductoCategoria
                {
                    CategoriaId = cateId,
                    ProductoId = producto.Id
                })
                .ToList();

            if (categoriasAInsertar.Any())
                await _repositorios.ProductosCategorias.AddRangeAsync(categoriasAInsertar);

            // --- Actualizar variantes ---
            foreach (var varianteDto in dto.Variants)
            {
                var variante = producto.Variants.FirstOrDefault(v => v.Id == varianteDto.Id);

                if (variante == null)
                {
                    // Nueva variante
                    variante = new ProductVariant
                    {
                        ProductoId = producto.Id,
                        SKU = varianteDto.SKU,
                        Talla = varianteDto.Talla,
                        Color = varianteDto.Color,
                        PrecioCosto = varianteDto.PrecioCosto,
                        PrecioVenta = varianteDto.PrecioVenta,
                        MonedaCostoId = varianteDto.MonedaCostoId,
                        MonedaVentaId = varianteDto.MonedaVentaId,
                        Stock = varianteDto.Stock
                    };
                    producto.Variants.Add(variante);
                }
                else
                {
                    // Actualizar variante existente
                    variante.SKU = varianteDto.SKU;
                    variante.Talla = varianteDto.Talla;
                    variante.Color = varianteDto.Color;
                    variante.PrecioCosto = varianteDto.PrecioCosto;
                    variante.PrecioVenta = varianteDto.PrecioVenta;
                    variante.MonedaCostoId = varianteDto.MonedaCostoId;
                    variante.MonedaVentaId = varianteDto.MonedaVentaId;
                    variante.Stock = varianteDto.Stock;
                }

                // --- Sincronización de fotos de la variante ---
                var nombresMantener = varianteDto.Fotos
                    .Select(f => Path.GetFileNameWithoutExtension(f.FileName))
                    .ToList();

                var fotosAEliminar = variante.Fotos
                    .Where(f => !nombresMantener.Contains(Path.GetFileNameWithoutExtension(f.Url)))
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
                                System.IO.File.Delete(filePath);
                        }
                    }
                    _repositorios.ProductosFotos.RemoveRange(fotosAEliminar);
                }

                // Procesar fotos nuevas
                if (varianteDto.Fotos != null && varianteDto.Fotos.Any())
                {
                    for (int i = 0; i < varianteDto.Fotos.Count; i++)
                    {
                        var file = varianteDto.Fotos[i];
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var path = Path.Combine("wwwroot", "uploads", "productos", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var url = $"/uploads/productos/{fileName}";

                        variante.Fotos.Add(new ProductoFoto
                        {
                            ProductVariantId = variante.Id,
                            Descripcion = file.FileName,
                            Orden = i,
                            EsPrincipal = (i == 0),
                            Url = url
                        });
                    }
                }
            }

            // EF ya trackea cambios en producto, variantes y fotos
            _repositorios.Productos.Update(producto);
            await _repositorios.SaveChangesAsync();

            return producto.Id;
        }


        //public async Task<Guid> ActualizarAsync(Guid id, ActualizarProductoInputDto dto, List<IFormFile> fotos)
        //{
        //    var nombresMantener = dto.FotosExistentes
        //        .Select(f => Path.GetFileNameWithoutExtension(f))
        //        .ToList();

        //    var producto = await _repositorios.Productos
        //        .GetQuery()
        //        .Include(p => p.Fotos)
        //        .Include(p => p.ProductoCategorias)
        //        .FirstOrDefaultAsync(p => p.Id == id);

        //    if (producto == null)
        //        throw new Exception("Producto no encontrado");

        //    // Actualizar propiedades simples
        //    producto.Codigo = dto.Codigo;
        //    producto.Descripcion = dto.Descripcion;
        //    producto.Color = dto.Color;
        //    producto.SKU = dto.SKU;
        //    producto.PrecioCosto = dto.PrecioCosto;
        //    producto.PrecioVenta = dto.PrecioVenta;
        //    producto.MonedaId = dto.MonedaId;
        //    producto.EsActivo = dto.EsActivo;

        //    // --- Sincronización de fotos ---
        //    var fotosAEliminar = producto.Fotos
        //        .Where(f => !nombresMantener.Contains(
        //            Path.GetFileNameWithoutExtension(f.Url)))
        //        .ToList();

        //    if (fotosAEliminar.Any())
        //    {
        //        foreach (var foto in fotosAEliminar)
        //        {
        //            if (!string.IsNullOrEmpty(foto.Url))
        //            {
        //                var filePath = Path.Combine(
        //                    Directory.GetCurrentDirectory(),
        //                    "wwwroot",
        //                    foto.Url.TrimStart('/')
        //                );

        //                if (System.IO.File.Exists(filePath))
        //                {
        //                    System.IO.File.Delete(filePath);
        //                }
        //            }
        //        }

        //        _repositorios.ProductosFotos.RemoveRange(fotosAEliminar);
        //    }

        //    // Procesar fotos nuevas
        //    if (fotos != null && fotos.Any())
        //    {
        //        for (int i = 0; i < fotos.Count; i++)
        //        {
        //            var file = fotos[i];
        //            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //            var path = Path.Combine("wwwroot", "uploads", "productos", fileName);

        //            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }

        //            var url = $"/uploads/productos/{fileName}";

        //            producto.Fotos.Add(new ProductoFoto
        //            {
        //                ProductoId = producto.Id,
        //                Descripcion = file.FileName,
        //                Orden = i,
        //                EsPrincipal = (i == 0),
        //                Url = url
        //            });
        //        }
        //    }

        //    // --- Sincronización de categorías ---
        //    var categoriasActuales = producto.ProductoCategorias.Select(c => c.CategoriaId).ToList();
        //    var categoriasNuevas = dto.CategoriaIds ?? new List<Guid>();

        //    // 1. Eliminar las que ya no están
        //    var categoriasAEliminar = producto.ProductoCategorias
        //        .Where(c => !categoriasNuevas.Contains(c.CategoriaId))
        //        .ToList();

        //    if (categoriasAEliminar.Any())
        //    {
        //        _repositorios.ProductosCategorias.RemoveRange(categoriasAEliminar);
        //    }

        //    // 2. Insertar las nuevas que no existían
        //    var categoriasAInsertar = categoriasNuevas
        //        .Where(cateId => !categoriasActuales.Contains(cateId))
        //        .Select(cateId => new ProductoCategoria
        //        {
        //            CategoriaId = cateId,
        //            ProductoId = producto.Id
        //        })
        //        .ToList();

        //    if (categoriasAInsertar.Any())
        //    {
        //        await _repositorios.ProductosCategorias.AddRangeAsync(categoriasAInsertar);
        //    }

        //    // Solo Update del producto, EF ya trackea las fotos nuevas
        //    _repositorios.Productos.Update(producto);

        //    await _repositorios.SaveChangesAsync();
        //    return producto.Id;
        //}

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

    }
}