using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Models
{
    public class User : BaseModel<string>
    {
        public string Name { get; set; }

        public string HashedPassword { get; set; }

        public string Email { get; set; }
    }
}
