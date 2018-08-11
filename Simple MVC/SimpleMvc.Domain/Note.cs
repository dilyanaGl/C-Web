using System.ComponentModel.DataAnnotations;

namespace SimpleMvc.Domain
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public int OwnerId { get; set; }

        [Required]
        public User Owner { get; set; }
    }
}