namespace API.Domain.Interfaces.Estadisticas
{
    public interface IKpiService
    {
        Task<object> GetKpis();
        Task<object> GetSellerKpis();
    }
}