using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronic
{
    public static class StringExtensions
    {
        public static string ReplaceAll(this string @this, string pattern, string replacement)
        {
            return Regex.Replace(@this, pattern, replacement);            
        }

        public static Regex Compile(this string @this)
        {
            return new Regex(@this, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string LastCharacters(this string @this, int numberOfCharsToTake)
        {
            if (@this == null)
            {
                return null;
            }

            if (@this.Length <= numberOfCharsToTake)
            {
                return @this;
            }

            return @this.Substring(@this.Length - numberOfCharsToTake);
        }

        public static string Numerize(this string @this)
        {
            return Numerizer.Numerize(@this);
        }

        /// <summary>
        /// Converts any numeric chars in input string (e.g. arabic) in to english numeric chars
        /// </summary>
        /// <param name="input">String that can contain non-english numeric chars</param>
        /// <returns>String, where non-english numeric chars replaced with english ones</returns>
        public static string ToStringWithEnglishNumbers(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var symbol in input)
            {
                if (char.IsDigit(symbol))
                {
                    var value = char.GetNumericValue(symbol);
                    sb.Append(value);
                }
                else
                {
                    var charCategory = Char.GetUnicodeCategory(symbol);
                    switch (charCategory)
                    {
                        case UnicodeCategory.Control:
                            break;
                        case UnicodeCategory.Format:
                            break;
                        default:
                            sb.Append(symbol);
                            break;
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Clears from string symbols that should not be used while date/time parsing
        /// </summary>
        /// <param name="input">Source string</param>
        /// <returns>Result string</returns>
        public static string ClearUnusedSymbols(this string input)
        {
            var sb = new StringBuilder();
            foreach (var symbol in input)
            {
                var charCategory = char.GetUnicodeCategory(symbol);
                switch (charCategory)
                {
                    case UnicodeCategory.Control:
                        break;
                    case UnicodeCategory.Format:
                        break;
                    default:
                        sb.Append(symbol);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
