using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DStutz.System.Extensions
{
    // See https://regex101.com
    public static class StringModifier
    {
        #region Methods SQL
        /***********************************************************/
        public static string ColumnName(
            this string self)
        {
            return self.ModifyAllUpperCasesButFirst("$1_$2").ToLower();
        }

        public static string TableName(
            this string self)
        {
            return self.ModifyAllUpperCasesButFirst("$1_$2").ToLower();
        }
        #endregion

        #region Methods upper cases
        /***********************************************************/
        public static string ModifyAllUpperCases(
            this string self,
            string replacement = "$1")
        {
            return Regex.Replace(self, "([A-Z])", replacement);
        }

        public static string ModifyAllUpperCasesButFirst(
            this string self,
            string replacement = "$1$2")
        {
            return Regex.Replace(self, "([a-z])([A-Z])", replacement);
        }
        #endregion

        #region Methods removing
        /***********************************************************/
        public static string RemoveWhiteSpaces(
            this string self)
        {
            return Regex.Replace(self, @"\s+", "");
        }

        public static string RemoveWhiteSpacesAndHyphens(
            this string self)
        {
            return Regex.Replace(self, @"\s+|-", "");
        }

        public static string RemoveEnding(
            this string self,
            params string[] endings)
        {
            foreach (var ending in endings)
                if (self.EndsWith(ending))
                    return self.Replace(ending, "");

            return self;
        }
        #endregion
    }
}
