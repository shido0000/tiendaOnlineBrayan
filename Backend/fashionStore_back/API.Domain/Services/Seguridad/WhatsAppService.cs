namespace API.Domain.Services.Seguridad
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class WhatsAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        private readonly string _phoneNumberId;

        public WhatsAppService(string accessToken, string phoneNumberId)
        {
            _httpClient = new HttpClient();
            _accessToken = accessToken;
            _phoneNumberId = phoneNumberId;
        }

        public async Task<bool> EnviarContrasennaAsync(string destino, string nuevaContrasenna)
        {
            var url = $"https://graph.facebook.com/v17.0/{_phoneNumberId}/messages";

            var payload = new
            {
                messaging_product = "whatsapp",
                to = destino, // número destino con prefijo internacional
                type = "text",
                text = new { body = $"Tu nueva contraseña temporal es: {nuevaContrasenna}\nPor favor cámbiala al iniciar sesión." }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}