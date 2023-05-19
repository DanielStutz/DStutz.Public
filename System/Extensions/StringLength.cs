namespace DStutz.System.Extensions
{
    public static class StringLength
    {
        #region Manipulating the length of a string
        /***********************************************************/
        public static string Fix(
            this string? self,
            int length)
        {
            if (self == null)
                return Spaces(length);

            if (self.Length > length)
                return self.Substring(0, length);

            if (self.Length < length)
                return self.PadRight(length);

            // self.Length = length
            return self;
        }
        #endregion

        #region Manipulating the length of an aligned string
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
                return Shorten(self, length, align);

            if (self.Length < length)
                return Extend(self, length, align); ;

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
                return value.PadRight(length, ' ');

            if (align == 'R')
                return value.PadLeft(length, ' ');

            throw new Exception("Align must be 'L' or 'R'");
        }

        private static string Dots(
            int length)
        {
            return "".PadRight(length, '.');
        }

        private static string Shorten(
            string value,
            int length,
            char align)
        {
            if (length <= 3)
                return Dots(length);

            if (align == 'L')
                return value.Substring(0, length - 3) + "...";

            if (align == 'R')
                return "..." + value.Substring(value.Length - length + 3, length - 3);

            throw new Exception("Align must be 'L' or 'R'");
        }

        private static string Spaces(
            int length)
        {
            return "".PadRight(length, ' ');
        }
        #endregion
    }
}
