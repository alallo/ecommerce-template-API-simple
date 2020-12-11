using System.Threading.Tasks;
using Ecommerce.EmailService;
using Ecommerce.Models;

namespace Ecommerce.Application
{
    public class OrderService: IOrderService
    {
        private readonly IEmailService _emailService;

        public OrderService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendOrderToAdminEmail(Order order, string email)
        {
            await _emailService.SendEmail<Order>(order, email);
        }
    }
}
