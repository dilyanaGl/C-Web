using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App.Data.Models
{
    public class Album : BaseModel<string>
    {
        public string Name { get; set; }

        public string Cover { get; set; }


        public virtual ICollection<AlbumTrack> Tracks { get; set; } = new List<AlbumTrack>();

        public decimal Price { get; set; }

            //get => Tracks.Select(p => p.Track.Price).Sum() * (decimal)(1 - 0.13); }




    }
}
