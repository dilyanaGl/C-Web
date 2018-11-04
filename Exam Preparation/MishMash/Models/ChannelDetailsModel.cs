using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class ChannelDetailsModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int FollowersCount { get; set; }

        public string Tags { get; set; }

        public string Description { get; set; }
    }
}
