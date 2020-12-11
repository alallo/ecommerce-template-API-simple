using System.Threading.Tasks;

namespace Ecommerce.EmailService
{
    public interface IEmailService
    {
        Task SendEmail<T>(T obj, string email);
    }
}
