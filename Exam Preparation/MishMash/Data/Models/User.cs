using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<UserChannel> ChannelsFollowing { get; set; } = new List<UserChannel>();
       
        public Role Role { get; set; }
    }
}
