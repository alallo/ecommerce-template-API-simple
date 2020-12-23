using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application
{
    public interface IOrderService
    {
        Task SendOrderToAdminEmail(Order order, string email, ILogger logger);
    }
}
