using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMvc.Domain
{
    public class User
    {
        public User()
        {
            this.Notes = new List<Note>();
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
