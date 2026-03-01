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
                new { Descripcion = SepararMayusculas(Talla.SinTalla.ToString()), Id = (int)Talla.SinTalla },
            };

            return Ok(tipos);
        }
        private string SepararMayusculas(string texto)
        {
            return System.Text.RegularExpressions.Regex.Replace(texto, "([a-z])([A-Z])", "$1 $2");
        }

    }
}
