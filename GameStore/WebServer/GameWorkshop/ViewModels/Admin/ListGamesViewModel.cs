using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.GameWorkshop.ViewModels.Admin
{
    public class ListGamesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Size{ get; set; }
    }
}
