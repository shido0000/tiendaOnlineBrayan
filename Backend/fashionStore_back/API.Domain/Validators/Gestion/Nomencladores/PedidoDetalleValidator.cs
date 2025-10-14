using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class PedidoDetalleValidator : AbstractValidator<PedidoDetalle>
    {
        private readonly IUnitOfWork<PedidoDetalle> _repositorios;

        public PedidoDetalleValidator(IUnitOfWork<PedidoDetalle> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.Cantidad).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.PrecioUnitario).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                    .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.DescuentoAplicado).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                  .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.EstadoLinea).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                    .NotNull().WithMessage("Es un campo obligatorio.");

        }
    }
}
