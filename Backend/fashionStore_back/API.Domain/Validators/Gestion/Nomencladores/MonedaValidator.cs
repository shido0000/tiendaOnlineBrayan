using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class MonedaValidator : AbstractValidator<Moneda>
    {
        private readonly IUnitOfWork<Moneda> _repositorios;

        public MonedaValidator(IUnitOfWork<Moneda> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(m => m.Codigo).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(10).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(100).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m.TasaCambio).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                  .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m).MustAsync(async (elemento, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Codigo == elemento.Codigo && e.Id != elemento.Id)))
             .WithMessage("Ya existe un Codigo con el mismo texto.");
            RuleFor(m => m).MustAsync(async (elemento, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Descripcion == elemento.Descripcion && e.Id != elemento.Id)))
            .WithMessage("Ya existe una Descripción con el mismo texto.");
        }
    }
}
