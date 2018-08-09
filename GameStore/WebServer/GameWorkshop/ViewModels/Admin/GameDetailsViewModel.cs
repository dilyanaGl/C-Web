using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.GameWorkshop.ViewModels.Admin
{
    public class GameDetailsViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string YouTubeViewId { get; set; }

        public double Size{ get; set; }

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
