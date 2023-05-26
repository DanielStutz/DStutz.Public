using System.Text.RegularExpressions;

namespace DStutz.System.Extensions;

// See https://regex101.com
public static class StringModifierSQL
{
    #region Methods handling names
    /***********************************************************/
    public static string ColumnName(
        this string self)
    {
        // Make 'ModelComment' to 'model_comment'
        return self.ModifyAllUpperCasesButFirst("$1_$2").ToLower();
    }

    public static string TableName(
        this string self)
    {
        // Make 'ModelComment' to 'model_comment'
        return self.ModifyAllUpperCasesButFirst("$1_$2").ToLower();
    }
    #endregion

    #region Methods parsing length of data types
    /***********************************************************/
    public static long TINYTEXT = 255;
    public static long TEXT = 65_535;
    public static long MEDIUMTEXT = 16_777_215;
    public static long LONGTEXT = 4_294_967_295;

    public static long DataTypeLength(
        this string textDataType)
    {
        textDataType = textDataType.RemoveWhiteSpaces().ToUpper();

        if (textDataType.StartsWith("CHAR"))
        {
            return textDataType.ParseDigits<long>(@"CHAR\(", @"\)");
        }
        else if (textDataType.StartsWith("VARCHAR"))
        {
            return textDataType.ParseDigits<long>(@"VARCHAR\(", @"\)");
        }
        else if (textDataType.Contains("TEXT"))
        {
            switch (textDataType)
            {
                case "TINYTEXT":
                    return TINYTEXT;
                case "TEXT":
                    return TEXT;
                case "MEDIUMTEXT":
                    return MEDIUMTEXT;
                case "LONGTEXT":
                    return LONGTEXT;
                default:
                    break;
            }
        }

        throw new Exception(
            $"Unknown MySQL data type '{textDataType}'");
    }
    #endregion

    #region Methods extracting and parsing
    /***********************************************************/
    private static string ExtractDigits(
        this string self,
        string start = "",
        string end = "")
    {
        // Extract 123 from 'start123end'
        return Regex.Replace(self, start + @"(\d+)" + end, "$1");
    }

    private static T ParseDigits<T>(
        this string self,
        string start = "",
        string end = "")
        where T : IParsable<T>
    {
        // Extract and parse 123 from 'start123end'
        return T.Parse(ExtractDigits(self, start, end), null);
    }
    #endregion
}
