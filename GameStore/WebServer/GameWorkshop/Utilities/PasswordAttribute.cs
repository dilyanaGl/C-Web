using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HTTPServer.GameWorkshop.Utilities
{
    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            this.ErrorMessage = "Password must contain at least one uppercase, one lowercase letter and one digit!";
        }

        public override bool IsValid(object value)
        {
            var password = value as string;
            if (password == null)
            {
                return true;
            }

            return password.Any(p => Char.IsLower(p))
                   && password.Any(p => Char.IsUpper(p))
                   && password.Any(p => Char.IsDigit(p));
        }
    }
}
