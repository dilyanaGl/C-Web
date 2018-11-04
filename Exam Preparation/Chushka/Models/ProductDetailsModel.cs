using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class ProductDetailsModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
