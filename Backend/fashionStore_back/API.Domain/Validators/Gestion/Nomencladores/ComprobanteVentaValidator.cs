using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class ComprobanteVentaValidator : AbstractValidator<ComprobanteVenta>
    {
        private readonly IUnitOfWork<ComprobanteVenta> _repositorios;

        public ComprobanteVentaValidator(IUnitOfWork<ComprobanteVenta> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.VentaId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.FechaEmision).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                       .NotNull().WithMessage("Es un campo obligatorio.");
            RuleFor(m => m.Numero).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                               .NotNull().WithMessage("Es un campo obligatorio.");

        }
    }
}
