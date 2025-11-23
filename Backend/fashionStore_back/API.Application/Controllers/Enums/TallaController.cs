using API.Data.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Reservacion.Enum
{

    [Route("api/[controller]")]
    [ApiController]
    public class TallaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            var tipos = new List<object>
            {
                new { Descripcion = Talla.XS.ToString(), Id = (int)Talla.XS },
                new { Descripcion = Talla.S.ToString(), Id = (int)Talla.S },
                new { Descripcion = Talla.M.ToString(), Id = (int)Talla.M },
                new { Descripcion = Talla.L.ToString(), Id = (int)Talla.L },
                new { Descripcion = Talla.XL.ToString(), Id = (int)Talla.XL },
            };

            return Ok(tipos);
        }
    }
}
