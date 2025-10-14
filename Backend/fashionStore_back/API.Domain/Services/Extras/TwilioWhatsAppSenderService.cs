using API.Domain.Interfaces.Extras;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;


namespace API.Domain.Services.Extras
{
    public class TwilioWhatsAppSender : IWhatsAppSender
    {
        public async Task SendTextAsync(string phone, string text)
        {
            await MessageResource.CreateAsync(
              body: text,
              from: new PhoneNumber("whatsapp:+123456789"),
              to: new PhoneNumber($"whatsapp:{phone}")
            );
        }

        //public async Task SendDocumentAsync(string phone, string caption, byte[] doc, string fname)
        //{
        //    var url = await UploadTemp(doc, fname);
        //    await MessageResource.CreateAsync(
        //      body: caption,
        //      from: new PhoneNumber("whatsapp:+123456789"),
        //      to: new PhoneNumber($"whatsapp:{phone}"),
        //      mediaUrl: new[] { new Uri(url) }
        //    );
        //}

        //private Task<string> UploadTemp(byte[] doc, string name) { /*…*/ }
    }
}