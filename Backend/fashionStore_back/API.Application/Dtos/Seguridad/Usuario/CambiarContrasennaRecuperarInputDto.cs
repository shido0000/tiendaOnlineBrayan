namespace API.Application.Dtos.Seguridad.Usuario
{
    public class CambiarContrasennaRecuperarInputDto
    {
        public required string Correo { get; set; }
        public required string NuevaContrasenna { get; set; }
        public required string ContrasennaConfirmada { get; set; }
    }
}
