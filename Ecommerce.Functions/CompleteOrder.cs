using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ecommerce.Models;
using System.Text.Json;
using Ecommerce.Application;

namespace Ecommerce.Functions
{
    public class CompleteOrder
    {
        private readonly IOrderService _orderService;
        public CompleteOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [FunctionName("CompleteOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "completeOrder")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var order = JsonSerializer.Deserialize<Order>(requestBody, options);
            await _orderService.SendOrderToAdminEmail(order, "alessandro.lallo@gmail.com");
            return new OkResult();
        }
    }
}
