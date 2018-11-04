using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chushka.Models
{
    public class ProductListModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Id { get; set; }

        public string DescriptionDisplay => 
            this.Description.Length > 20 ?
            this.Description.Substring(20) + "..."
            : this.Description;
    }
}
