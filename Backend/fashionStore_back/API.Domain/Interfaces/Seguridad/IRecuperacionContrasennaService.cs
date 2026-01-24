namespace API.Domain.Interfaces.Seguridad
{
    public interface IRecuperacionContrasennaService
    {
        Task<string> RecuperarContrasennaAsync(string correo);
        Task<bool> CorreoExistente(string correo);
    }
}