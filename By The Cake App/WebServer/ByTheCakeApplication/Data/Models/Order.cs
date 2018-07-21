using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.ByTheCakeApplication.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
