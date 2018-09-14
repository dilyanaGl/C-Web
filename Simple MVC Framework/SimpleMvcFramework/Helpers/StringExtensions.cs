using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Helpers
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return word;
            }
            return String.Concat(Char.ToUpper(word[0]), word.Substring(1));
        }
    }
}
