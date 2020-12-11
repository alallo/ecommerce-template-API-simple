using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Order
    {
        public List <Basket> Basket { get; set; }
        public Customer Customer { get; set; }
    }
}