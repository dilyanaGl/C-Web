using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.GameWorkshop.ViewModels.Admin
{
    public class HomeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        public string ImageThumbnail { get; set; }
    }
}
