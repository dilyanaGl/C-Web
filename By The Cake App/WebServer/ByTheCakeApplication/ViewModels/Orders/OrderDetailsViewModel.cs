using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.ByTheCakeApplication.ViewModels.Orders
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
    }
}
