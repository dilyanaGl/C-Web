using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();


    }
}
