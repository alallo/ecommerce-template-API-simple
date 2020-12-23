using System;
using System.Linq;
using Ecommerce.Models;

namespace Ecommerce.Application
{
    public static class EmailTemplateHelper
    {
        public static string GenerateEmailOrderToAdminBody(Order order)
        {
            var template = new HtmlTemplate(@"HtmlTemplates/OrderCompleteAdmin.html");
            var output = template.Render(new {
                Name = order.Customer.Name,
                Email = order.Customer.Email,
                Telephone = order.Customer.Telephone,
                Address = order.Customer.Address,
                DeliveryDate = order.Customer.DeliveryDate,
                Basket = String.Join(", ", order.Basket.Select(x => $"Product: {x.Product.Id} - {x.Product.Name}, Quantity: {x.Quantity}"))
            });
            return output;
        }
    }
}
