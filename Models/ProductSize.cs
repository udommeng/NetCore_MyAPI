using System;
using System.Collections.Generic;

namespace MyAPI.Models
{
    public partial class ProductSize
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }

        public Products Product { get; set; }
    }
}
