using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class MensajeriaValidator : AbstractValidator<Mensajeria>
    {
        private readonly IUnitOfWork<Mensajeria> _repositorios;

        public MensajeriaValidator(IUnitOfWork<Mensajeria> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.MonedaId).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.");

            RuleFor(m => m.Descripcion).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(100).WithMessage("Debe tener {MaxLength} caracteres máximo.");

            RuleFor(m => m).MustAsync(async (Mensajerias, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Descripcion == Mensajerias.Descripcion && e.Id != Mensajerias.Id)))
            .WithMessage("Ya existe una Descripción con el mismo texto.");
        }
    }
}
