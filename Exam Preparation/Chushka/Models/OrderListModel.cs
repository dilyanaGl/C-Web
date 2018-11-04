using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class OrderListModel
    {
        public int Id { get; set; }

        public String Customer { get; set; }

        public string Product { get; set; }

        public string OrderedOn { get; set; }
    }
}
