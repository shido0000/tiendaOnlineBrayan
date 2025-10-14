using API.Domain.Interfaces.Extras;
using System.Net.Mail;


namespace API.Domain.Services.Extras
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpClient _smtp;
        public SmtpEmailSender(SmtpClient smtp) => _smtp = smtp;

        public async Task SendEmailAsync(string to, string subject, string htmlBody, byte[] attachment, string filename)
        {
            var msg = new MailMessage("no-reply@store.com", to, subject, htmlBody) { IsBodyHtml = true };
            if (attachment != null)
                msg.Attachments.Add(new Attachment(new MemoryStream(attachment), filename));
            await _smtp.SendMailAsync(msg);
        }
    }
}