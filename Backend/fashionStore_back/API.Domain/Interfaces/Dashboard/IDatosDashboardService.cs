using API.Data.Dto.Dashboard;

namespace API.Domain.Interfaces.Dashboard
{
    public interface IDatosDashboardService
    {
        Task<DashboardDto> GetDashboardDataAsync();
    }
}