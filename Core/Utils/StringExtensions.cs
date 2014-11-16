using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cqrs.Core.Utils
{
    public static class StringExtensions
    {
        public static Boolean IsNullOrBlank(this String str)
        {
            return str == null || str.Trim() == String.Empty;
        }
        public static Boolean IsNotNullOrBlank(this String str)
        {
            return !(str == null || str.Trim() == String.Empty);
        }
        public static String BlankToNull(this String str)
        {
            return str == null || str.Trim() == String.Empty ? null : str;
        }
        public static String LimitToLength(this String str, int size, bool? max = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length <= size)
                return str;
            var textPreShorten = str.Substring(0, size - 3 + 1);
            var lastSpace = Math.Max(textPreShorten.LastIndexOf(" "), textPreShorten.LastIndexOf("\n"));
            var noLastSpace = lastSpace == -1;
            var shorter = "";
            if (noLastSpace)
                shorter = str.Substring(0, size);
            else
                shorter = str.Substring(0, lastSpace);
            return shorter + "...";
        }
        public static String Replace(this Match match, String what, String with)
        {
            var from = match.Captures[0].Index;
            var to = from + match.Captures[0].Length;
            var newStr = what.Substring(0, from) + with + what.Substring(to);
            return newStr;
        }
        public static String Before(this String str, String before)
        {
            if (str == null)
                return null;
            var indexOf = str.IndexOf(before);
            if (indexOf == -1)
                return null;
            return str.Substring(0, indexOf);
        }
        public static String BeforeOrAll(this String str, String before)
        {
            if (str == null)
                return null;
            var indexOf = str.IndexOf(before);
            if (indexOf == -1)
                return str;
            return str.Substring(0, indexOf);
        }
        public static String After(this String str, String after)
        {
            if (str == null)
                return null;
            var indexOf = str.IndexOf(after);
            if (indexOf == -1)
                return null;
            return str.Substring(indexOf + after.Length);
        }
        public static String AfterOrAll(this String str, String after)
        {
            if (str == null)
                return null;
            var indexOf = str.IndexOf(after);
            if (indexOf == -1)
                return str;
            return str.Substring(indexOf + after.Length);
        }
        private static Regex WHITE_SPACE = new Regex(@"\s+");
        public static String SingleSpace(this String str)
        {
            if (str == null)
                return str;
            return WHITE_SPACE.Replace(str, " ");
        }

        public static String Replace(this String str, Regex regex, String with)
        {
            if (str.IsNullOrBlank())
                return null;
            return regex.Replace(str, with);
        }

        public static string CleanUrl(this string str)
        {
            str = Regex.Replace(str, "[^a-zA-Z0-9 ]", "");
            str = Regex.Replace(str, " +", "_");
            return str;
        }
        public static string CutFromEnd(this string str, int lengthToCut)
        {
            return str.Substring(0, str.Length - lengthToCut);
        }
        public static String TakeUntil(this string str, string until)
        {
            var index = str.IndexOf(until);
            if (index == -1)
                return str;
            return str.Substring(0, index);
        }

        public static bool IsEmail(this string strIn)
        {
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper);
            }
            catch
            {
                return false;
            }

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();
            var domainName = match.Groups[2].Value;
            domainName = idn.GetAscii(domainName);
            return match.Groups[1].Value + domainName;
        }

        public static string ToUrl(this string strIn)
        {
            return strIn.ToLower().Replace(" ", "-");
        }

        public static string Fmt(this string formatTemplate, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, formatTemplate, args);
        }

        public static string RemoveQueryStringFromUri(this string uri)
        {
            int index = uri.IndexOf('?');
            if (index > -1)
            {
                uri = uri.Substring(0, index);
            }
            return uri;
        }

        public static string ReplaceToken(this string body, string token, object value)
        {
            return body.Replace("{" + token + "}", value.ToString());
        }


    }
}
