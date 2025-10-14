namespace API.Domain.Interfaces.Extras
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string htmlBody, byte[] attachment, string filename);
    }
}