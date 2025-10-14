using API.Data.Entidades.Inicio;

namespace API.Domain.Interfaces.Inicio
{
    public interface IDatosInicioService
    {
        Task<DatosInicio> ObtenerDatosInicio();
    }
}