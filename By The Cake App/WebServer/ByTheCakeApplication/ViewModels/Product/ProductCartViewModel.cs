using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HTTPServer.ByTheCakeApplication.ViewModels.Product
{
    public class ProductCartViewModel
    {
       public string Name { get; set; }

        public decimal Price { get; set; }

        
    }
}
