using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Gestion.Nomencladores
{
    public class BannerPromocionValidator : AbstractValidator<BannerPromocion>
    {

        private readonly IUnitOfWork<BannerPromocion> _repositorios;

        public BannerPromocionValidator(IUnitOfWork<BannerPromocion> repositorios)
        {

            _repositorios = repositorios;

            RuleFor(x => x.TextoBoton)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(20).WithMessage("El nombre no puede exceder 20 caracteres");

            RuleFor(x => x.Imagen)
                .NotEmpty().WithMessage("La imagen es requerida");

            RuleFor(x => x.TextoTitulo)
                .MaximumLength(300).WithMessage("El título no puede exceder 300 caracteres");

            RuleFor(x => x.TextoSubtitulo)
                .MaximumLength(500).WithMessage("El subtítulo no puede exceder 500 caracteres");
        }
    }
}