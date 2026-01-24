using API.Application.Dtos.Comunes;
using API.Application.Dtos.Seguridad.Autenticacion;
using API.Application.Dtos.Seguridad.Recuperacion;
using API.Application.Filters;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Seguridad;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Application.Controllers.Seguridad
{

    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManagerFilter))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AutenticacionController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly IAutenticacionService _autenticacionServicio;
        protected readonly IUsuarioService _usuarioService;
        protected readonly IBackgroundJobClient _clientHangfire;
        protected readonly IRecuperacionContrasennaService _RecuperacionContrasennaService;

        public AutenticacionController(IMapper mapper, IAutenticacionService autenticacionServicio, IBackgroundJobClient clientHangfire, IUsuarioService usuarioService, IRecuperacionContrasennaService recuperacionContrasennaService)
        {
            _autenticacionServicio = autenticacionServicio;
            _mapper = mapper;
            _clientHangfire = clientHangfire;
            _usuarioService = usuarioService;
            _RecuperacionContrasennaService = recuperacionContrasennaService;
        }


        /// <summary>
        /// Retorna informacion del usuario logiado
        /// </summary>
        [HttpGet("[action]")]
        public ActionResult ObtenerInformacionUsuario()
            => Ok(new ResponseDto { Status = StatusCodes.Status200OK, Result = User.Identity?.Name });

        /// <summary>
        /// Inicia sesion de un usuario
        /// </summary>
        /// <response code="200">Retorna true si se cambio la contraseña</response>
        /// <response code="400">Retorna el mensaje del error ocurrido</response>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginInputDto login)
        {
            if (await _autenticacionServicio.Login(login.Username, login.Contrasenna))
            {
                (string token, DateTime fechaExpiracion) = await _autenticacionServicio.ConstruirToken(login.Username);

                await _usuarioService.GuardarTraza(login.Username, $"{login.Username} ha inciado sesion.", "Sesion");

                LoginOutputDto result = new() { FechaExpiracion = fechaExpiracion, Token = token };
                return Ok(new ResponseDto { Status = StatusCodes.Status200OK, Result = result });
            }
            else
                return Unauthorized(new ResponseDto { Status = StatusCodes.Status401Unauthorized, ErrorMessage = "Usuario o contraseña no válido." });
        }

        [HttpGet("UsuarioActual")]
        [Authorize] // requiere token válido
        public async Task<ActionResult> ObtenerUsuarioActual()
        {
            // El claim UniqueName lo pusiste como el username
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
                return Unauthorized(new ResponseDto { Status = StatusCodes.Status401Unauthorized, ErrorMessage = "Token inválido." });

            // Buscar info del usuario en tu servicio/repositorio
            var usuario = await _usuarioService.ObtenerPorUsername(username);

            if (usuario == null)
                return NotFound(new ResponseDto { Status = StatusCodes.Status404NotFound, ErrorMessage = "Usuario no encontrado." });

            return Ok(new ResponseDto
            {
                Status = StatusCodes.Status200OK,
                Result = new
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    NombreCompleto = usuario.NombreCompleto,
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    Correo = usuario.Correo,
                    Telefono = usuario.Telefono,
                    RolNombre = usuario.Rol.Nombre,
                    RolId = usuario.Rol.Id,
                    Permisos = await _usuarioService.ObtenerPermisos(username)
                }
            });
        }

        //[HttpPost("Recuperar")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Recuperar([FromBody] RecuperarContrasenhaRequest recuperarContrasenhaRequest)
        //{
        //    var resultado = await _RecuperacionContrasennaService.RecuperarContrasennaAsync(recuperarContrasenhaRequest.Correo);
        //    return Ok(new { NuevaContrasenna = resultado });
        //}


        [HttpPost("VerificarCorreo")]
        [AllowAnonymous]
        public async Task<IActionResult> VerificarCorreo([FromBody] RecuperarContrasenhaRequest request)
        {
            var existe = await _RecuperacionContrasennaService.CorreoExistente(request.Correo);
            if(!existe) throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "No existe usuario con ese correo." };
            return Ok(new { Existe = existe });
        }
    }
}
