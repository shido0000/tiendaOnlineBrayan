using API.Application.Dtos.Seguridad.Usuario;
using FluentValidation;

namespace API.Application.Validadotors.Seguridad
{
    public class CambiarContrasennaRecuperarInputDtoValidator : AbstractValidator<CambiarContrasennaRecuperarInputDto>
    {

        public CambiarContrasennaRecuperarInputDtoValidator()
        {
            RuleFor(m => m).Must((usuario, cancelacion) => usuario.NuevaContrasenna == usuario.ContrasennaConfirmada).OverridePropertyName(nameof(CambiarContrasennaRecuperarInputDto.NuevaContrasenna)).WithMessage("Las contraseñas no coinciden.");
        }
    }
}
