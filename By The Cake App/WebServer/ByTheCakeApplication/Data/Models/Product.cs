using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HTTPServer.ByTheCakeApplication.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImageUrl { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}