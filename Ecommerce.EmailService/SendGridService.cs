using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Ecommerce.EmailService
{
    public class SendGridService: IEmailService
    {
        private readonly Settings _appSettings;

        public SendGridService(Settings appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task SendEmail(string htmlBody, string plainText, string email, ILogger logger)
        {
            try
            {
                var apiKey = _appSettings.SendGridApiKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("alessandro.lallo@gmail.com", "Ecommerce");
                var subject = "A new order has been completed";
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlBody);
                var response = await client.SendEmailAsync(msg);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Failed to send order email to admin.");
            }
        }
    }
}
