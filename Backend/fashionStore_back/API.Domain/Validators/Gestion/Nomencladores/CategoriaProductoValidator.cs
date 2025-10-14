using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class CategoriaProductoValidator : AbstractValidator<CategoriaProducto>
    {
        private readonly IUnitOfWork<CategoriaProducto> _repositorios;

        public CategoriaProductoValidator(IUnitOfWork<CategoriaProducto> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.Nombre).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(50).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(100).WithMessage("Debe tener {MaxLength} caracteres máximo.");

             
            RuleFor(m => m).MustAsync(async (CategoriaProductos, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Nombre == CategoriaProductos.Nombre && e.Id != CategoriaProductos.Id)))
            .WithMessage("Ya existe un Nombre con el mismo texto.");
            RuleFor(m => m).MustAsync(async (CategoriaProductos, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Descripcion == CategoriaProductos.Descripcion && e.Id != CategoriaProductos.Id)))
            .WithMessage("Ya existe una Descripción con el mismo texto.");
        }
    }
}
