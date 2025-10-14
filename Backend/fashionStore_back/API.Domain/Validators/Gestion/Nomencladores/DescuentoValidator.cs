using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class DescuentoValidator : AbstractValidator<Descuento>
    {
        private readonly IUnitOfWork<Descuento> _repositorios;

        public DescuentoValidator(IUnitOfWork<Descuento> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(m => m.Nombre).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                  .NotNull().WithMessage("Es un campo obligatorio.")
                                  .MaximumLength(100).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m.Porcentaje).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.MontoFijo).NotEmpty().WithMessage("No puede ser un texto vacio.")
                         .NotNull().WithMessage("Es un campo obligatorio.");
        }
    }
}
