using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Data.Dto.Usuario
{
    /// <summary>
    /// Tabla que guarda datos de los usuarios del sistema
    /// </summary>
    public class UsuarioActualizarDto
    {
        public required Guid Id { get; set; }

        public required string Nombre { get; set; }
        public required string Apellidos { get; set; }
        public required string Username { get; set; }
        public required string Contrasenna { get; set; }
        public required string Correo { get; set; }
        public required string Telefono { get; set; }
    }
}