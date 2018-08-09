using HTTPServer.GameWorkshop.Utilities;

namespace HTTPServer.GameWorkshop.ViewModels.Account
{
    using Infrastructure;
    using System.ComponentModel.DataAnnotations;


    public class RegisterViewModel
    {
        [Required]
        [MaxLength(ValidationConstants.Account.EmailMaxLength,
            ErrorMessage = ValidationConstants.Account.EmailTooLong)]
        [RegularExpression(@"(^\w+)@(\w+)\.(\w+)$",
            ErrorMessage = ValidationConstants.Account.InvalidFormat)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLength,
            ErrorMessage = ValidationConstants.Account.PasswordTooShort)]
        [MaxLength(ValidationConstants.Account.PasswordMaxLength,
            ErrorMessage = ValidationConstants.Account.PasswordTooLong)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")]
        [Password]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [MinLength(ValidationConstants.Account.NameMinLength,
            ErrorMessage = ValidationConstants.Account.NameTooShort)]
        [MaxLength(ValidationConstants.Account.NameMaxLength,
            ErrorMessage = ValidationConstants.Account.NameTooLong)]
        public string FullName { get; set; }
    }
}
