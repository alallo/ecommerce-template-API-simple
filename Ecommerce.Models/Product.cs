using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Product
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }
            public bool IsFeatured { get; set; }
            public List <string> Images { get; set; }
    }
}