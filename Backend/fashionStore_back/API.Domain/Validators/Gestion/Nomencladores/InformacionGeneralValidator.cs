using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    public class InformacionGeneralValidator : AbstractValidator<InformacionGeneral>
    {

        private readonly IUnitOfWork<InformacionGeneral> _repositorios;

        public InformacionGeneralValidator(IUnitOfWork<InformacionGeneral> repositorios)
        {
            _repositorios = repositorios;
        }
    }
}