namespace DStutz.System.Stringers
{
    public abstract class Reader
    {
        #region Methods reading optional
        /***********************************************************/
        public static string? Read(
            string? value,
            string? replacement = null)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (replacement != null)
                value = value.Replace(replacement, "");

            return value;
        }

        public static string? Read(
            string? value,
            params (string OldValue, string NewValue)[] replacements)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            foreach (var item in replacements)
                if (value.Equals(item.OldValue))
                    return item.NewValue;

            return value;
        }

        #endregion
        #region Methods reading mandatory
        /***********************************************************/
        public static string ReadOrThrow(
            string value,
            string? replacement = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Value is null or empty");

            if (replacement != null)
                value = value.Replace(replacement, "");

            return value;
        }

        public static string ReadOrThrow(
            string value,
            params (string OldValue, string NewValue)[] replacements)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Value is null or empty");

            foreach (var item in replacements)
                if (value.Equals(item.OldValue))
                    return item.NewValue;

            return value;
        }
        #endregion
    }
}
