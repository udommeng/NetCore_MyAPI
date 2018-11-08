using System;
using System.Collections.Generic;

namespace MyAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
