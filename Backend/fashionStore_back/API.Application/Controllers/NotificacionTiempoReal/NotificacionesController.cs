using API.Data.Dto.Notificacion;
using API.Domain.Interfaces.NotificacionTiempoReal;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.NotificacionTiempoReal
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionPedidoService _notificacionService;

        public NotificacionesController(INotificacionPedidoService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpPost("enviar-notificacion")]
        public async Task<IActionResult> EnviarNotificacion([FromBody] NotificacionPedidoDto datosNotificacion)
        {
            await _notificacionService.EnviarNotificacionPedidoAsync(datosNotificacion);
            return Ok(new { mensaje = "Notificación enviada" });
        }
    }
}