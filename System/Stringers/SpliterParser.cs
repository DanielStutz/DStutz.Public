namespace DStutz.System.Stringers
{
    public class SpliterParser
    {
        #region Methods splitting and parsing optional
        /***********************************************************/
        public static IEnumerable<T>? Split<T>(
            string? value,
            char separator,
            params string[] replacements)
            where T : IParsable<T>
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            foreach (var item in replacements)
                value = value.Replace(item, "");

            var array = value.Split(separator);

            for (int i = 0; i < array.Length; i++)
                array[i] = array[i].Trim();

            return array.Select(e => T.Parse(e, null));
        }
        #endregion

        #region Methods splitting and parsing mandatory
        /***********************************************************/
        public static IEnumerable<T> SplitOrThrow<T>(
            string? value,
            char separator,
            params string[] replacements)
            where T : IParsable<T>
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Value is null or empty");

            foreach (var item in replacements)
                value = value.Replace(item, "");

            var array = value.Split(separator);

            for (int i = 0; i < array.Length; i++)
                array[i] = array[i].Trim();

            return array.Select(e => T.Parse(e, null));
        }
        #endregion
    }
}
