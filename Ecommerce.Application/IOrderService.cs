using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Application
{
    public interface IOrderService
    {
        Task SendOrderToAdminEmail(Order order, string email);
    }
}
