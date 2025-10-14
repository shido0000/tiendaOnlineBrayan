using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class ProductoValidator : AbstractValidator<Producto>
    {
        private readonly IUnitOfWork<Producto> _repositorios;

        public ProductoValidator(IUnitOfWork<Producto> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(m => m.Codigo).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                    .NotNull().WithMessage("Es un campo obligatorio.")
                                    .MaximumLength(50).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(200).WithMessage("Debe tener {MaxLength} caracteres máximo.");


            RuleFor(m => m.Color).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.SKU).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                    .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.PrecioCosto).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.PrecioVenta).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                    .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m).MustAsync(async (elemento, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Codigo == elemento.Codigo && e.Id != elemento.Id)))
          .WithMessage("Ya existe un Codigo con el mismo texto.");
        }

    }
}
