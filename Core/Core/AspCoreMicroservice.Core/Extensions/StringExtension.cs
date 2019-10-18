using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace AspCoreMicroservice.Core.Extensions
{
    public static class StringExtensions
    {
        static readonly Regex re = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);
        public static string ReplaceMatch(this string text, StringDictionary fields)
        {
            return re.Replace(text, match => fields[match.Groups[1].Value]);
        }

        public static bool IsBase64String(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
                || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                // Handle the exception
            }
            return false;
        }

        public static Stream ToMemoryStream(this string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            return new MemoryStream(bytes, 0, bytes.Length);
        }
    }
}
