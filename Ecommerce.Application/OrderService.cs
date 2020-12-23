using System.Threading.Tasks;
using Ecommerce.EmailService;
using Ecommerce.Models;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application
{
    public class OrderService: IOrderService
    {
        private readonly IEmailService _emailService;

        public OrderService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendOrderToAdminEmail(Order order, string email, ILogger logger)
        {
            var htmlBody = EmailTemplateHelper.GenerateEmailOrderToAdminBody(order);
            await _emailService.SendEmail(htmlBody, "", email, logger);
        }
    }
}
