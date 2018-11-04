using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class ListChannelsModel
    {
        public ListChannelModel[] Following { get; set; }

        public ListChannelModel[] Suggested { get; set; }

        public ListChannelModel[] SeeOther { get; set; }
    }
}
