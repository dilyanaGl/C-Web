using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ProductType { get; set; }
    }
}
