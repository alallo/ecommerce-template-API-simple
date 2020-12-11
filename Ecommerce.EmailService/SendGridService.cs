using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.Json;

namespace Ecommerce.EmailService
{
    public class SendGridService: IEmailService
    {
        private readonly Settings _appSettings;

        public SendGridService(Settings appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task SendEmail<T>(T obj, string email)
        {
            try
            {
                var apiKey = _appSettings.SendGridApiKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("alessandro.lallo@gmail.com", "Ecommerce");
                var subject = "A new order has been completed";
                var to = new EmailAddress(email);
                var plainTextContent = JsonSerializer.Serialize<T>(obj);
                var htmlContent = JsonSerializer.Serialize<T>(obj);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
