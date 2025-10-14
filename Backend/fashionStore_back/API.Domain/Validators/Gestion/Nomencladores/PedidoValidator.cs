using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        private readonly IUnitOfWork<Pedido> _repositorios;

        public PedidoValidator(IUnitOfWork<Pedido> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(m => m.Total).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.Estado).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                    .NotNull().WithMessage("Es un campo obligatorio.");

        }
    }
}
