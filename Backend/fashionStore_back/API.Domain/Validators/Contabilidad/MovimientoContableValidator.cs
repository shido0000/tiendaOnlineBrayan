using API.Data.Entidades.Contabilidad;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Contabilidad
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class MovimientoContableValidator : AbstractValidator<MovimientoContable>
    {
        private readonly IUnitOfWork<MovimientoContable> _repositorios;

        public MovimientoContableValidator(IUnitOfWork<MovimientoContable> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.AsientoContableId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.CuentaContableId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                         .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.Debe).NotEmpty().WithMessage("No puede ser un texto vacio.")
                         .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.Haber).NotEmpty().WithMessage("No puede ser un texto vacio.")
                         .NotNull().WithMessage("Es un campo obligatorio.");
        }
    }
}
