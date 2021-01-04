using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Ecommerce.Models;
using System.Text.Json;

namespace Ecommerce.Functions
{
    public class GetProducts
    {
        [FunctionName("GetProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var productList = await GetProductFromFileAsync(context);
            return new OkObjectResult(productList);
        }

        
        private async Task<IList<Product>> GetProductFromFileAsync(ExecutionContext context)
        {
            using (StreamReader r = new StreamReader(Path.Combine(context.FunctionAppDirectory, "productList.json")))
            {
                string json = await r.ReadToEndAsync();
                var productList = JsonSerializer.Deserialize<List<Product>>(json);
                return productList;
            }
        }
    }

}
