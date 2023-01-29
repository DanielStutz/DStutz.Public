namespace DStutz.System.Extensions
{
    public static class StringLength
    {
        #region Methods manipulating the length of a string
        /***********************************************************/
        public static string Max(
            this string? self,
            int length,
            char align = 'L')
        {
            if (self == null)
                return Spaces(length);

            if (self.Length > length)
                return Shorten(self, length, align);

            // self.Length <= length
            return self;
        }

        public static string Fix(
            this string? self,
            int length,
            char align = 'L')
        {
            if (self == null)
                return Spaces(length);

            if (self.Length > length)
                return self.Max(length, align);

            if (self.Length < length)
                return self.Min(length, align);

            // self.Length = length
            return self;
        }

        public static string Min(
            this string? self,
            int length,
            char align = 'L')
        {
            if (self == null)
                return Spaces(length);

            if (self.Length < length)
                return Extend(self, length, align);

            // self.Length >= lengthh
            return self;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private static string Extend(
            string value,
            int length,
            char align)
        {
            if (align == 'L')
                return string.Format("{0,-" + length + "}", value);

            if (align == 'R')
                return string.Format("{0," + length + "}", value);

            throw new Exception("Align must be 'L' or 'R'");
        }

        private static string Shorten(
            string value,
            int length,
            char align)
        {
            if (align == 'L')
                return value.Substring(0, length - 3) + "...";

            if (align == 'R')
                return "..." + value.Substring(value.Length - length + 3, length - 3);

            throw new Exception("Align must be 'L' or 'R'");
        }

        private static string Spaces(
            int length)
        {
            return string.Format("{0," + length + "}", "");
        }
        #endregion
    }
}
