using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ecommerce.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(string htmlBody, string plainText, string email, ILogger logger);
    }
}
