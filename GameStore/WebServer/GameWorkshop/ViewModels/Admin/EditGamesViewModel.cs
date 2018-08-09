using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HTTPServer.GameWorkshop.Infrastructure;

namespace HTTPServer.GameWorkshop.ViewModels.Admin
{
   public class EditGamesViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(ValidationConstants.Game.TitleMinLength,
            ErrorMessage = ValidationConstants.Game.TitleTooShort)]
        [MaxLength(ValidationConstants.Game.TitleMaxLength,
            ErrorMessage = ValidationConstants.Game.TitleTooLong)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.TitleMaxLength,
            ErrorMessage = ValidationConstants.Game.InvalidUrl)]
        [MaxLength(ValidationConstants.Game.UrlLength)]
        public string YouTubeVideoId { get; set; }

        [Required]
        public string ImageThumbnail { get; set; }

        public double Size { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.DescriptionMinLength,
            ErrorMessage = ValidationConstants.Game.DescriptionTooShort)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
