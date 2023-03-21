namespace DStutz.System.Stringers
{
    public abstract class Spliter
    {
        #region Methods splitting optional
        /***********************************************************/
        public static string[]? Split(
            string? value,
            char separator)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var array = value.Split(separator);

            for (int i = 0; i < array.Length; i++)
                array[i] = array[i].Trim();

            return array;
        }

        public static string[][]? Split(
            string? value,
            char separator1,
            char separator2)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var array1 = value.Split(separator1);
            var array2 = new string[array1.Length][];

            for (int i = 0; i < array2.Length; i++)
                array2[i] = Split(array1[i], separator2)!;

            return array2;
        }
        #endregion

        #region Methods splitting mandatory
        /***********************************************************/
        public static string[] SplitOrThrow(
            string? value,
            char separator)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Value is null or empty");

            var array = value.Split(separator);

            for (int i = 0; i < array.Length; i++)
                array[i] = array[i].Trim();

            return array;
        }

        public static string[][] SplitOrThrow(
            string? value,
            char separator1,
            char separator2)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Value is null or empty");

            var array1 = value.Split(separator1);
            var array2 = new string[array1.Length][];

            for (int i = 0; i < array2.Length; i++)
                array2[i] = Split(array1[i], separator2)!;

            return array2;
        }
        #endregion
    }
}
