using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HTTPServer.GameWorkshop.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HTTPServer.GameWorkshop.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [RegularExpression(@"^(\w+)@(\w+)\.(\w+)$")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(25)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")]
        public string Password { get; set; }

        public string FullName { get; set; }

        public ICollection<UserGame> Games { get; set; } = new List<UserGame>();

        public bool IsAdmin { get; set; } = false;

    }
}
