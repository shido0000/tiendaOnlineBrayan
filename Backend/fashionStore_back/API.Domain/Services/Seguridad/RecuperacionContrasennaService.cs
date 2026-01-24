using API.Data.ClasesAuxiliares.Correo;
using API.Data.ClasesAuxiliares.WhatsApp;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;

namespace API.Domain.Services.Seguridad
{
    public class RecuperacionContrasennaService : IRecuperacionContrasennaService
    {
        readonly IUnitOfWork<Usuario> _repositorios;
        private readonly SmtpOptions _smtpSettings;
        private readonly WhatsAppOptions _whatsAppSettings;

        public RecuperacionContrasennaService(IUnitOfWork<Usuario> repositorios, IHttpContextAccessor httpContext, IOptions<SmtpOptions> smtpSettings, IOptions<WhatsAppOptions> whatsAppSettings)
        {
            _repositorios = repositorios;
            _smtpSettings = smtpSettings.Value;
            _whatsAppSettings = whatsAppSettings.Value;
        }

        public async Task<string> RecuperarContrasennaAsync(string correo)
        {
            var usuario = await _repositorios.Usuarios
                .GetQuery()
                .FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null)
                return "";

            // Generar nueva contraseña temporal
            var nuevaContrasenna = GenerarContrasennaTemporal();

            // Guardar en la BD (idealmente encriptada/hasheada)
            usuario.Contrasenna = Crypto.HashPassword(nuevaContrasenna);
            usuario.DebeCambiarContrasenna = true;

            await _repositorios.SaveChangesAsync();

            // Enviar correo
           // EnviarCorreo(usuario.Correo, nuevaContrasenna);

            return nuevaContrasenna;
        }

        private string GenerarContrasennaTemporal()
        {
            return Guid.NewGuid().ToString("N")[..8]; // 8 caracteres aleatorios
        }


        private void EnviarCorreo(string destino, string nuevaContrasenna)
        {
            var mensaje = new MailMessage();
            mensaje.To.Add(destino);
            mensaje.Subject = "Recuperación de contraseña";
            mensaje.Body = $"Tu nueva contraseña temporal es: {nuevaContrasenna}\n" +
                           "Por favor cámbiala al iniciar sesión.";
            mensaje.IsBodyHtml = false;
            // mensaje.From = new MailAddress(_smtpSettings.From);
            mensaje.From = new MailAddress(_smtpSettings.User);

            using var smtp = new SmtpClient(_smtpSettings.Host)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.User, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };

            smtp.Send(mensaje);
        }

        public async Task<bool> EnviarWhatsApp(string correo, string telefonoUsuario)
        {
            var usuario = await _repositorios.Usuarios
                .GetQuery()
                .FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null)
                return false;

            var nuevaContrasenna = GenerarContrasennaTemporal();
            usuario.Contrasenna = Crypto.HashPassword(nuevaContrasenna);
            usuario.DebeCambiarContrasenna = true;

            await _repositorios.SaveChangesAsync();

            // Enviar por WhatsApp
            var whatsapp = new WhatsAppService(_whatsAppSettings.AccessToken, _whatsAppSettings.PhoneNumberId);
            await whatsapp.EnviarContrasennaAsync(telefonoUsuario, nuevaContrasenna);

            return true;
        }


        public async Task<bool> CorreoExistente(string correo) {
            return await _repositorios.Usuarios.AnyAsync(e => e.Correo == correo);
        }
    }
}