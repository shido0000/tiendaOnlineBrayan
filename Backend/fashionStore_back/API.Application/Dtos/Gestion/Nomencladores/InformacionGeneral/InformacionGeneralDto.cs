using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.InformacionGeneral
{
    public class InformacionGeneralDto : EntidadBaseDto
    {
        public string? SobreNosotros { get; set; }
        public string? Privacidad { get; set; }
        public string? Terminos { get; set; }
        public string? Devoluciones { get; set; }
        public string? Colabora { get; set; }
        public string? DireccionTienda { get; set; }
        public string? TelefonoTienda { get; set; }
        public string? HorarioTienda { get; set; }
        public string? EnlaceTelegram { get; set; }
        public string? EnlaceFacebook { get; set; }
        public string? EnlaceWhatsapp { get; set; }
        public string? EnlaceInstagram { get; set; }
    }
}