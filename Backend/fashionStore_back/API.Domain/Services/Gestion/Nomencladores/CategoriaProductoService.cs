using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class CategoriaProductoService : BasicService<CategoriaProducto, CategoriaProductoValidator>, ICategoriaProductoService
    {

        public CategoriaProductoService(IUnitOfWork<CategoriaProducto> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        public async Task<Guid> CrearCategoriaAsync(CategoriaProducto dto, IFormFile? foto)
        {
            var categoria = new CategoriaProducto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            if (foto != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(foto.FileName)}";
                var path = Path.Combine("wwwroot", "uploads", "categorias", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                categoria.FotoUrl = $"/uploads/categorias/{fileName}";
            }

            await _repositorios.CategoriasProductos.AddAsync(categoria);
            await _repositorios.SaveChangesAsync();

            return categoria.Id;
        }

        public async Task<Guid> ActualizarCategoriaAsync(Guid id, CategoriaProducto dto, IFormFile? foto)
        {
            var categoria = await _repositorios.CategoriasProductos
                .GetQuery()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
                throw new Exception("Categoría no encontrada");

            // Actualizar propiedades simples
            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;

            // Si viene una nueva foto
            if (foto != null)
            {
                // 1. Eliminar la foto anterior del servidor si existe
                if (!string.IsNullOrEmpty(categoria.FotoUrl))
                {
                    var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        categoria.FotoUrl.TrimStart('/')
                    );

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                // 2. Guardar la nueva foto
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(foto.FileName)}";
                var path = Path.Combine("wwwroot", "uploads", "categorias", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                categoria.FotoUrl = $"/uploads/categorias/{fileName}";
            }

            _repositorios.CategoriasProductos.Update(categoria);
            await _repositorios.SaveChangesAsync();

            return categoria.Id;
        }


    }
}