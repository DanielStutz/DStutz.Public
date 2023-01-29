using System.Text.RegularExpressions;

namespace DStutz.System.Stringers
{
    public abstract class Pattern
    {
        public static string NO_SPACE = " ";
        public static string NO_WHITE_SPACES = @"\s+";
    }

    public abstract class Parser<T>
    {
        public string? Pattern { get; set; }
        public string? Separator { get; set; }

        /************************************************************
         * Methods - Parse
         ************************************************************/
        public abstract T Parse(string input);

        public List<T> SplitAndParse(
            string input)
        {
            if (Separator == null)
                throw new Exception("No separator set");

            return SplitAndParse(input, Separator);
        }

        public List<T> SplitAndParse(
            string input,
            string? separator)
        {
            if (Pattern == null)
                throw new Exception("No pattern set");

            return SplitAndParse(input, Pattern, separator);
        }

        public List<T> SplitAndParse(
            string input,
            string? pattern,
            string? separator)
        {
            input = Replace(input, pattern);

            var list = new List<T>();

            foreach (var element in input.Split(separator))
                list.Add(Parse(element));

            return list;
        }

        /************************************************************
         * Methods - Replace
         ************************************************************/
        public string Replace(
            string input)
        {
            if (Pattern == null)
                throw new Exception("No pattern set");

            return Replace(input, Pattern);
        }

        public string Replace(
            string input,
            string? pattern)
        {
            if (pattern == null)
                return input;

            return Regex.Replace(input, pattern, "");
        }
    }

    public class ParserInt16 : Parser<short>
    {
        public override short Parse(string input)
        {
            return short.Parse(input);
        }
    }

    public class ParserInt32 : Parser<int>
    {
        public override int Parse(string input)
        {
            return int.Parse(input);
        }
    }

    public class ParserInt64 : Parser<long>
    {
        public override long Parse(string input)
        {
            return long.Parse(input);
        }
    }
}
