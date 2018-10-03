using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Extensions
{
    public class StringExtension
    {
        public string Capitalize(string word)
        {
            return $"{Char.ToUpper(word[0])}{word.Substring(1)}";

        }

    }
}
