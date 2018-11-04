using System;
using System.Collections.Generic;
using System.Text;

namespace Turshia.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public String Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
