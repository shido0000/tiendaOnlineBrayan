using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class InventarioValidator : AbstractValidator<Inventario>
    {
        private readonly IUnitOfWork<Inventario> _repositorios;

        public InventarioValidator(IUnitOfWork<Inventario> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(m => m.CantidadDisponible).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.CantidadReservada).NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.Ubicacion).MaximumLength(100).WithMessage("Debe tener {MaxLength} caracteres máximo.");
            RuleFor(m => m.EstadoProductoInventario).NotNull().WithMessage("Debe tener un estado el producto.");

        }
    }
}
