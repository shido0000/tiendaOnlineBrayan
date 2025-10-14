namespace API.Domain.Interfaces.Extras
{
    public interface IWhatsAppSender
    {
        Task SendTextAsync(string phoneNumber, string text);
        //Task SendDocumentAsync(string phoneNumber, string caption, byte[] document, string filename);
    }
}