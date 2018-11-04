using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class EditProductViewModel
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public string ProductType { get; set; }

        public int Id { get; set; }
    }
}
