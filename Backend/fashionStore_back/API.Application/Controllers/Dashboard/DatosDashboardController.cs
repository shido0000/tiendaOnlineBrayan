using API.Data.Dto.Dashboard;
using API.Domain.Interfaces.Dashboard;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosDashboardController : ControllerBase
    {
        private readonly IDatosDashboardService _DatosDashboardService;

        public DatosDashboardController(IDatosDashboardService datosDashboardService)
        {
            _DatosDashboardService = datosDashboardService;
        }

        /// <summary>
        /// Obtiene los datos del dashboard
        /// </summary>
        /// <returns>Datos del dashboard</returns>
        [HttpGet]
        [Route("GetDashboardData")]
        public async Task<ActionResult<DashboardDto>> GetDashboardData()
        {
            var data = await _DatosDashboardService.GetDashboardDataAsync();
            return Ok(data);
        }
    }
}
