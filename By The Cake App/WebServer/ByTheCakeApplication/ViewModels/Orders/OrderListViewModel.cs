using System;

namespace HTTPServer.ByTheCakeApplication.ViewModels.Orders
{
    public class OrderListViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Sum { get; set; }

    }
}
