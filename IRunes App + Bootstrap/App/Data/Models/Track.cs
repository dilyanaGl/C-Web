using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Models
{
    public class Track : BaseModel<string>
    {
        //[Required]
        //[StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }


        //[Required]
        //[StringLength(2000, MinimumLength = 3)]
        public string Link { get; set; }

        //[Reuqired]
        //[Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public virtual ICollection<AlbumTrack> Albums { get; set; } = new HashSet<AlbumTrack>();
    }
}
