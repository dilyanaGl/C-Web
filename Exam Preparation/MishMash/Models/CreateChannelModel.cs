using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class CreateChannelModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string Type { get; set; }
    }
}
