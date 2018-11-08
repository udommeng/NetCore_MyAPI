using System;
using System.Collections.Generic;

namespace MyAPI.Models
{
    public partial class Products
    {
        public Products()
        {
            ProductSize = new HashSet<ProductSize>();
        }

        public int ProductId { get; set; }
        public string CodeName { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string Timestamp { get; set; }

        public Category Category { get; set; }
        public ICollection<ProductSize> ProductSize { get; set; }
    }
}
