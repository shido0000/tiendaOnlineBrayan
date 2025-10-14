using API.Data.Entidades.Contabilidad;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Contabilidad
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class AsientoContableValidator : AbstractValidator<AsientoContable>
    {
        private readonly IUnitOfWork<AsientoContable> _repositorios;

        public AsientoContableValidator(IUnitOfWork<AsientoContable> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.Fecha).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");
            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");
            RuleFor(m => m.ReferenciaId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");
            RuleFor(m => m.TipoReferencia).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");
        }
    }
}
