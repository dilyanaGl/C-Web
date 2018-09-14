using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.App.Data.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public User Owner { get; set; }
    }
}
