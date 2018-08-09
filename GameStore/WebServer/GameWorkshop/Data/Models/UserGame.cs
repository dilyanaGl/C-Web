using System.ComponentModel.DataAnnotations;

namespace HTTPServer.GameWorkshop.Data.Models
{
    public class UserGame
    {
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; }
    }
}