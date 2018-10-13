using System;
using System.Collections.Generic;
using System.Text;

namespace App.ViewModels
{
    public class TrackDetailsModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Link { get; set; }

        public string AlbumId { get; set; }
    }
}
