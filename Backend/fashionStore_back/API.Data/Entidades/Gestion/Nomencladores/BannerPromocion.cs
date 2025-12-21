namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class BannerPromocion : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        // Imagen
        public string Imagen { get; set; } = string.Empty; // ruta o URL

        // Texto opcional
        public string? TextoTitulo { get; set; }
        public string? TextoSubtitulo { get; set; }

        // Botón CTA
        public string? BotonTexto { get; set; }
        public string? BotonVinculo { get; set; } // ruta o URL externa

        // Control
        public bool EsActivo { get; set; } = true;
        public bool Destacado { get; set; } = false;
        public int Orden { get; set; } = 1;

        // Ubicaciones (separadas por coma: "home,productos,categorias")
        public string Ubicaciones { get; set; }

        // Dispositivos (separadas por coma: "desktop,mobile,tablet")
        public string? Dispositivos { get; set; }

        // Fechas de campaña
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}