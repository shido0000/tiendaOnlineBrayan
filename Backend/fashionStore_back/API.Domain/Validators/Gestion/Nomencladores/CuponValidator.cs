using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class CuponValidator : AbstractValidator<Cupon>
    {
        private readonly IUnitOfWork<Cupon> _repositorios;

        public CuponValidator(IUnitOfWork<Cupon> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.Codigo).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(50).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(100).WithMessage("Debe tener {MaxLength} caracteres máximo.");


            RuleFor(m => m.FechaInicio).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");
            RuleFor(m => m.FechaFin).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                  .NotNull().WithMessage("Es un campo obligatorio.");
            RuleFor(m => m.MaximoUsos).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                              .NotNull().WithMessage("Es un campo obligatorio.");
            RuleFor(m => m.UsosActuales).NotNull().WithMessage("Es un campo obligatorio.");



            RuleFor(m => m).MustAsync(async (Cupons, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Codigo == Cupons.Codigo && e.Id != Cupons.Id)))
            .WithMessage("Ya existe un Codigo con el mismo texto.");
            RuleFor(m => m).MustAsync(async (Cupons, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Descripcion == Cupons.Descripcion && e.Id != Cupons.Id)))
            .WithMessage("Ya existe una Descripción con el mismo texto.");
        }
    }
}
