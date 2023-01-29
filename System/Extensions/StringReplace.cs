using System.Text.RegularExpressions;

namespace DStutz.System.Extensions
{
    public static class StringReplace
    {
        public static string ReplaceWhiteSpaces(
            this string self)
        {
            // Remove all whitespaces and '-'
            return Regex.Replace(self, @"\s+|-", "");
        }
    }
}
