namespace API.Domain.Interfaces.Seguridad
{
    public interface IAutenticacionService
    {
        Task<(string, DateTime)> ConstruirToken(string username);
        Task<bool> Login(string userName, string contrasenna);

        /// <summary>
        /// Almacena el token en caché hasta su expiración
        /// <param name="token">token</param>
        /// </summary>
        Task ListaNegraTokenAsync(string token);
        /// <summary>
        /// Chequea si el token está en la lista negra
        /// <param name="token">token</param>
        /// </summary>
        bool EnListaNegraTokenAsync(string token);
    }
}