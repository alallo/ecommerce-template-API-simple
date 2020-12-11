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
            ILogger log)
        {
            var productList = await GetProductFromFileAsync();
            return new OkObjectResult(productList);
        }

        
        private async Task<IList<Product>> GetProductFromFileAsync()
        {
            using (StreamReader r = new StreamReader("productList.json"))
            {
                string json = await r.ReadToEndAsync();
                var productList = JsonSerializer.Deserialize<List<Product>>(json);
                return productList;
            }
        }
    }

}
