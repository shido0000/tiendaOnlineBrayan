using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class GestorValidator : AbstractValidator<Gestor>
    {
        private readonly IUnitOfWork<Gestor> _repositorios;

        public GestorValidator(IUnitOfWork<Gestor> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(m => m.Nombre).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                  .NotNull().WithMessage("Es un campo obligatorio.")
                                  .MaximumLength(50).WithMessage("Debe tener {MaxLength} caracteres máximo.");


            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(100).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m).MustAsync(async (Gestors, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Descripcion == Gestors.Descripcion && e.Id != Gestors.Id)))
            .WithMessage("Ya existe una Descripción con el mismo texto.");
        }
    }
}
