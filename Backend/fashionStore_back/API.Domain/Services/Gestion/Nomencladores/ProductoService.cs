using API.Data.Dto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class ProductoService : BasicService<Producto, ProductoValidator>, IProductoService
    {
        public ProductoService(IUnitOfWork<Producto> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        public async Task<Guid> CrearProducto(ProductoACrear producto)
        {
            var nuevoProducto = new Producto()
            {
                Id = Guid.NewGuid(),
                Codigo = producto.Codigo,
                Descripcion = producto.Descripcion,
                EsActivo = producto.EsActivo,
                SKU = producto.SKU,
                PrecioCosto = producto.PrecioCosto,
                PrecioVenta = producto.PrecioVenta,
                MonedaCostoId = producto.MonedaCostoId,
                MonedaVentaId = producto.MonedaVentaId,
                StockTotal = 0,
            };

            var listaProductosOtrasVariantes = new List<OtraVarianteProductoVariante>();
            var listaProductosCategorias = new List<ProductoCategoria>();
            var listaProductosVariantes = new List<ProductoVariante>();
            foreach (var catId in producto.CategoriasIds)
            {
                var nuevoProductoCategoria = new ProductoCategoria()
                {
                    CategoriaId = catId,
                    ProductoId = nuevoProducto.Id,
                };
                listaProductosCategorias.Add(nuevoProductoCategoria);
            }

            foreach (var variante in producto.ProductoVariantes)
            {
                var nuevoProductoVariante = new ProductoVariante()
                {

                    Id = Guid.NewGuid(),
                    ProductoId = nuevoProducto.Id,
                    Talla = variante.Talla,
                    Color = variante.Color,
                    Stock = variante.Stock,
                    Principal = variante.Principal,
                    // Fotos = variante.Fotos,
                };

                for (int i = 0; i < variante.Fotos.Count; i++)
                {
                    var file = variante.Fotos[i];
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine("wwwroot", "uploads", "productos", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var url = $"/uploads/productos/{fileName}";

                    nuevoProductoVariante.Fotos.Add(new ProductoFoto
                    {
                        ProductoVarianteId = nuevoProductoVariante.Id,
                        Descripcion = file.FileName,   // 👈 nombre original del archivo
                        Orden = i,                     // 👈 orden según posición en la lista
                        EsPrincipal = (i == 0),        // 👈 solo la primera es principal
                        Url = url
                    });
                }
                foreach (var variantId in variante.OtrasVariantesIds) {
                    var nuevaOtraVariante = new OtraVarianteProductoVariante()
                    {
                        Id = Guid.NewGuid(),
                        OtraVarianteId = variantId,
                        ProductoVarianteId = variante.ProductoId,
                    };
                    listaProductosOtrasVariantes.Add(nuevaOtraVariante);
                }

                listaProductosVariantes.Add(nuevoProductoVariante);
            }

            nuevoProducto.StockTotal = listaProductosVariantes.Sum(e => e.Stock);

            await _repositorios.Productos.AddAsync(nuevoProducto);
            await _repositorios.ProductosCategorias.AddRangeAsync(listaProductosCategorias);
            await _repositorios.ProductoVariantes.AddRangeAsync(listaProductosVariantes);
            await _repositorios.OtraVarianteProductoVariantes.AddRangeAsync(listaProductosOtrasVariantes);
            await _repositorios.SaveChangesAsync();

            return nuevoProducto.Id;
        }

        public async Task<Guid> ActualizarProducto(Guid productoId, ProductoAActualizar producto)
        {
            var productoExistente = await _repositorios.Productos
                .GetQuery()
                .Include(p => p.ProductoCategorias)
                .Include(p => p.ProductosVariantes)
                    .ThenInclude(v => v.Fotos)
                .Include(p => p.ProductosVariantes)
                    .ThenInclude(v => v.OtraVarianteProductoVariantes)
                .FirstOrDefaultAsync(e => e.Id == productoId)
                ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Producto no encontrado." };

            // --- Actualizar propiedades simples ---
            productoExistente.Codigo = producto.Codigo;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.EsActivo = producto.EsActivo;
            productoExistente.SKU = producto.SKU;
            productoExistente.PrecioCosto = producto.PrecioCosto;
            productoExistente.PrecioVenta = producto.PrecioVenta;
            productoExistente.MonedaCostoId = producto.MonedaCostoId;
            productoExistente.MonedaVentaId = producto.MonedaVentaId;

            // --- Sincronizar Categorías ---
            var categoriasExistentesIds = productoExistente.ProductoCategorias.Select(pc => pc.CategoriaId).ToList();
            //var otrasVariantesProductosVariantesExistentesIds = productoExistente.ProductosVariantes.Select(pc => pc.OtraVarianteProductoVariantes.Select(e=>e.OtraVarianteId)).ToList();
              
            var categoriasAEliminar = productoExistente.ProductoCategorias
                .Where(pc => !producto.CategoriasIds.Contains(pc.CategoriaId))
                .ToList();
            _repositorios.ProductosCategorias.RemoveRange(categoriasAEliminar);

            var categoriasANuevas = producto.CategoriasIds
                .Where(catId => !categoriasExistentesIds.Contains(catId))
                .Select(catId => new ProductoCategoria
                {
                    ProductoId = productoExistente.Id,
                    CategoriaId = catId
                }).ToList();
            await _repositorios.ProductosCategorias.AddRangeAsync(categoriasANuevas);

            // --- Sincronizar Variantes ---
            var variantesExistentes = productoExistente.ProductosVariantes.ToList();

            // Eliminar variantes que ya no vienen
            var variantesAEliminar = variantesExistentes
                .Where(v => !producto.ProductoVariantes.Any(pv => pv.ProductoId == v.Id))
                .ToList();
            _repositorios.ProductoVariantes.RemoveRange(variantesAEliminar);

            var nuevasVariantes = new List<ProductoVariante>();
            var listaProductosOtrasVariantes = new List<OtraVarianteProductoVariante>();


            foreach (var varianteDto in producto.ProductoVariantes)
            {
                var varianteExistente = variantesExistentes.FirstOrDefault(v => v.Id == varianteDto.ProductoId);

                if (varianteExistente != null)
                {
                    // Actualizar existente
                    varianteExistente.Talla = varianteDto.Talla;
                    varianteExistente.Color = varianteDto.Color;
                    varianteExistente.Stock = varianteDto.Stock;
                    varianteExistente.Principal = varianteDto.Principal;

                    // --- Sincronizar fotos ---
                    // Eliminar fotos que ya no vienen (comparando por Id)
                    var fotosAEliminar = varianteExistente.Fotos
                        .Where(f => !varianteDto.FotosExistentesIds.Contains(f.Id))
                        .ToList();
                    _repositorios.ProductosFotos.RemoveRange(fotosAEliminar);

                    // Agregar nuevas fotos
                    foreach (var file in varianteDto.FotosNuevas)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                            var filePath = Path.Combine("wwwroot/uploads/productos", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            varianteExistente.Fotos.Add(new ProductoFoto
                            {
                                ProductoVarianteId = varianteExistente.Id,
                                Url = $"/uploads/productos/{fileName}",
                                Descripcion = file.FileName,
                                EsPrincipal = false,
                                Orden = varianteExistente.Fotos.Count
                            });
                        }
                    }

                    // --- Sincronizar otras variantes ---
                    var existentesOtrasIds = varianteExistente.OtraVarianteProductoVariantes
                        .Select(ov => ov.OtraVarianteId)
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();

                    var nuevasOtrasIds = varianteDto.OtrasVariantesIds;

                    // Eliminar relaciones que ya no vienen
                    var otrasAEliminar = varianteExistente.OtraVarianteProductoVariantes
                        .Where(ov => ov.OtraVarianteId.HasValue && !nuevasOtrasIds.Contains(ov.OtraVarianteId.Value))
                        .ToList();
                    _repositorios.OtraVarianteProductoVariantes.RemoveRange(otrasAEliminar);

                    // Agregar nuevas relaciones
                    var otrasANuevas = nuevasOtrasIds
                        .Where(id => !existentesOtrasIds.Contains(id))
                        .Select(id => new OtraVarianteProductoVariante
                        {
                            ProductoVarianteId = varianteExistente.Id,
                            OtraVarianteId = id
                        }).ToList();
                    await _repositorios.OtraVarianteProductoVariantes.AddRangeAsync(otrasANuevas);
                }
                else
                {
                    // Crear nueva variante
                    var nuevaVariante = new ProductoVariante
                    {
                        ProductoId = productoExistente.Id,
                        Talla = varianteDto.Talla,
                        Color = varianteDto.Color,
                        Stock = varianteDto.Stock,
                        Principal = varianteDto.Principal,
                        Fotos = new List<ProductoFoto>()
                    };

                    // Guardar fotos nuevas
                    foreach (var file in varianteDto.FotosNuevas)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                            var filePath = Path.Combine("wwwroot/uploads/productos", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            nuevaVariante.Fotos.Add(new ProductoFoto
                            {
                                Url = $"/uploads/productos/{fileName}",
                                Descripcion = file.FileName,
                                EsPrincipal = false,
                                Orden = nuevaVariante.Fotos.Count
                            });
                        }
                    }

                    // Agregar relaciones de otras variantes nuevas
                    nuevaVariante.OtraVarianteProductoVariantes = varianteDto.OtrasVariantesIds
                        .Select(id => new OtraVarianteProductoVariante
                        {
                            OtraVarianteId = id
                        }).ToList();

                    nuevasVariantes.Add(nuevaVariante);
                }

                
            }

            if (nuevasVariantes.Any())
            {
                await _repositorios.ProductoVariantes.AddRangeAsync(nuevasVariantes);
            }

            // --- Recalcular stock total ---
            productoExistente.StockTotal = producto.ProductoVariantes.Sum(v => v.Stock);

            _repositorios.Productos.Update(productoExistente);
            await _repositorios.SaveChangesAsync();

            return productoExistente.Id;
        }



        public async Task<ProductoEspecificoDto> ObtenerProductoEspecifico(Guid id)
        {
            var producto = await _repositorios.Productos
                            .GetQuery()
                            .AsNoTracking()
                            .Include(e => e.ProductosVariantes)
                                .ThenInclude(e => e.Fotos)
                            .Include(e => e.ProductoCategorias)
                                .ThenInclude(e => e.Categoria)
                            .Include(e => e.ProductosVariantes)
                                .ThenInclude(e => e.OtraVarianteProductoVariantes)
                            .Include(e => e.ProductosVariantes)
                                .ThenInclude(e => e.OtraVarianteProductoVariantes)
                                     .ThenInclude(e => e.OtraVariante)
                            .FirstOrDefaultAsync(e => e.Id == id)
                            ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Producto no encontrado." };

            var productoDevolver = new ProductoEspecificoDto()
            {
                Id=producto.Id,
                Codigo = producto.Codigo,
                Descripcion = producto.Descripcion,
                EsActivo = producto.EsActivo,
                SKU = producto.SKU,
                PrecioCosto = producto.PrecioCosto,
                PrecioVenta = producto.PrecioVenta,

                MonedaCostoId = producto.MonedaCostoId,
                MonedaVentaId = producto.MonedaVentaId,
                StockTotal = producto.StockTotal,
                CategoriasIds = producto.ProductoCategorias.Select(e => e.CategoriaId).ToList(),
                ProductoVariantes = new(),
            };

            foreach (var variant in producto.ProductosVariantes) {
                productoDevolver.ProductoVariantes.Add(new ProductoEspecificoVarianteDto
                {
                    Id = variant.Id,
                    ProductoId = variant.ProductoId,
                    Talla = variant.Talla,
                    Color = variant.Color,
                    Stock = variant.Stock,
                    Principal = variant.Principal,
                    OtrasVariantesIds = variant.OtraVarianteProductoVariantes.Select(e => e.OtraVarianteId.Value).ToList(),
                    Fotos = variant.Fotos.Select(f => new ProductoFotoDto
                    {
                        Id = f.Id,
                        ProductVariantId = f.ProductoVarianteId,
                        Url = f.Url,
                        Descripcion = f.Descripcion,
                        EsPrincipal = f.EsPrincipal,
                        Orden = f.Orden,
                    }).ToList()
                });
            }

            return productoDevolver;
        }

        public async Task<List<Producto>> ObtenerProductosNovedades()
        {
            var lista = await _repositorios.Productos
                            .GetQuery()
                            .AsNoTracking()
                            .Include(e => e.ProductosVariantes)
                                .ThenInclude(e => e.Fotos)
                            .Include(e => e.ProductoCategorias)
                                .ThenInclude(e => e.Categoria)
                            .ToListAsync();

            var hoy = DateTime.Today; // solo la fecha, sin hora
            var desde = hoy.AddDays(-10);

            lista = lista.Where(e => e.FechaCreado.Date >= desde && e.FechaCreado.Date <= hoy).ToList();

            return lista;
        }

        //public async Task<List<ProductosAgrupados>> ObtenerProductosAgrupados()
        //{
        //    var productos = await _repositorios.Productos
        //        .GetQuery()
        //        .AsNoTracking()
        //        .Include(p => p.ProductoCategorias)
        //        .Include(p => p.Fotos)
        //        .ToListAsync();

        //    var agrupados = productos
        //        .Where(p => !string.IsNullOrEmpty(p.SKU) && p.SKU.Length >= 2)
        //        .GroupBy(p => p.SKU.Substring(0, 2)) // 👈 agrupar por los 2 primeros caracteres
        //        .Select(g => new ProductosAgrupados
        //        {
        //            SKU = g.Key, // el prefijo de 2 caracteres
        //            Codigo = g.First().Codigo,
        //            Descripcion = g.First().Descripcion,
        //            EsActivo = g.First().EsActivo,
        //            PrecioVenta = g.First().PrecioVenta,
        //            MonedaVenta = g.First().MonedaVenta?.Descripcion, // si MonedaVenta tiene campo Descripcion
        //            Stock = g.Sum(p => p.Stock), // sumar stock de todos los productos del grupo
        //            Talla = g.Select(p => p.Talla).Where(t => !string.IsNullOrEmpty(t)).Distinct().ToList(),
        //            Color = g.Select(p => p.Color).Where(c => !string.IsNullOrEmpty(c)).Distinct().ToList(),
        //            CategoriaIds = g.SelectMany(p => p.ProductoCategorias.Select(pc => pc.CategoriaId)).Distinct().ToList(),
        //            // si quieres traer fotos, aquí deberías mapearlas a IFormFile o a otra estructura
        //            Fotos = new List<IFormFile>() // placeholder, depende de cómo quieras mapear ProductoFoto
        //        })
        //        .ToList();

        //    return agrupados;
        //}

        //public async Task<ProductosAgrupados?> ObtenerProductoAgrupadoPorSku(string sku)
        //{
        //    if (string.IsNullOrEmpty(sku) || sku.Length < 2)
        //        return null;

        //    var prefijo = sku.Substring(0, 2);

        //    var productos = await _repositorios.Productos
        //        .GetQuery()
        //        .AsNoTracking()
        //        .Include(p => p.ProductoCategorias)
        //        .Include(p => p.Fotos)
        //        .Where(p => p.SKU != null && p.SKU.StartsWith(prefijo)) // 👈 filtrar por prefijo
        //        .ToListAsync();

        //    if (!productos.Any())
        //        return null;

        //    var agrupado = new ProductosAgrupados
        //    {
        //        SKU = prefijo,
        //        Codigo = productos.First().Codigo,
        //        Descripcion = productos.First().Descripcion,
        //        EsActivo = productos.First().EsActivo,
        //        PrecioVenta = productos.First().PrecioVenta,
        //        MonedaVenta = productos.First().MonedaVenta?.Descripcion,
        //        Stock = productos.Sum(p => p.Stock),
        //        Talla = productos.Select(p => p.Talla)
        //                         .Where(t => !string.IsNullOrEmpty(t))
        //                         .Distinct()
        //                         .ToList(),
        //        Color = productos.Select(p => p.Color)
        //                         .Where(c => !string.IsNullOrEmpty(c))
        //                         .Distinct()
        //                         .ToList(),
        //        CategoriaIds = productos.SelectMany(p => p.ProductoCategorias
        //                                                  .Select(pc => pc.CategoriaId))
        //                                .Distinct()
        //                                .ToList(),
        //        Fotos = new List<IFormFile>() // aquí deberías mapear tus ProductoFoto si lo necesitas
        //    };

        //    return agrupado;
        //}

    }
}