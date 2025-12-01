namespace API.Application.Dtos.Gestion.Nomencladores.Mensajeria
{
    public class ListadoPaginadoMensajeriaDto : DetallesMensajeriaDto
    {
        public required string TextoMensajeriaPrecio { get; set; }
        public required string MonedaCodigo { get; set; }
    }
}
