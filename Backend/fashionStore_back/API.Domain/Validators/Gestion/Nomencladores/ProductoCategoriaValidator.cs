using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class ProductoCategoriaValidator : AbstractValidator<ProductoCategoria>
    {
        private readonly IUnitOfWork<ProductoCategoria> _repositorios;

        public ProductoCategoriaValidator(IUnitOfWork<ProductoCategoria> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(m => m.ProductoId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.CategoriaId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                    .NotNull().WithMessage("Es un campo obligatorio.");

          
        }

    }
}
