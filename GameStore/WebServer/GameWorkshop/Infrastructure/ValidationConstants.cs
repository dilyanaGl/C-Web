using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTTPServer.GameWorkshop.Infrastructure
{
    public class ValidationConstants
    {
        public class Account
        {
            public const int EmailMaxLength = 30;
            public const string EmailTooLong = "Email must not be longer than 30 symbols!";
            public const string InvalidFormat = "Email is not valid!";

            public const int PasswordMinLength = 6;
            public const string PasswordTooShort = "Password must be at least 6 symbols!";
            public const int PasswordMaxLength = 50;
            public const string PasswordTooLong = "Password must not be longer than 50 symbols!";

            public const int NameMinLength = 2;
            public const string NameTooShort = "Name must be at least 2 characters!";
            public const int NameMaxLength = 30;
            public const string NameTooLong = "Name must not be longer than 30 characters!";
        }

        public class Game
        {
            public const int TitleMinLength = 3;
            public const string TitleTooShort = "Title must be at least 3 characters!";
            public const int TitleMaxLength = 100;
            public const string TitleTooLong = "Title must be less than 100 characters!";

            public const int UrlLength = 11;
            public const string InvalidUrl = "Url must be 11 characters long!";

            public const int DescriptionMinLength = 20;
            public const string DescriptionTooShort = "Description must be at least 20 characters long!";



        }
    }
}
