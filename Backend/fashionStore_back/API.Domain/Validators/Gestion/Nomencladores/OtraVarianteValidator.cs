using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class OtraVarianteValidator : AbstractValidator<OtraVariante>
    {
        private readonly IUnitOfWork<OtraVariante> _repositorios;

        public OtraVarianteValidator(IUnitOfWork<OtraVariante> repositorios)
        {
            _repositorios = repositorios;


            RuleFor(m => m.Nombre).NotEmpty().WithMessage("No puede ser un texto vacio.")
                                     .NotNull().WithMessage("Es un campo obligatorio.")
                                     .MaximumLength(50).WithMessage("Debe tener {MaxLength} caracteres máximo.");

         
             
            RuleFor(m => m).MustAsync(async (OtraVariantes, cancelacion) => !(await _repositorios.BasicRepository.AnyAsync(e => e.Nombre == OtraVariantes.Nombre && e.Id != OtraVariantes.Id)))
            .WithMessage("Ya existe un Nombre con el mismo texto.");
               }
    }
}
