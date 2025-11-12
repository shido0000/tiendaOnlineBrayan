using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class ListaDeseosDetalleValidator : AbstractValidator<ListaDeseosDetalle>
    {
        private readonly IUnitOfWork<ListaDeseosDetalle> _repositorios;

        public ListaDeseosDetalleValidator(IUnitOfWork<ListaDeseosDetalle> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.ProductVariantId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");
        }
    }
}
