namespace DStutz.Data
{
    // See https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
    public abstract class PolyglotText
    {
        #region Methods
        /***********************************************************/
        public static string Find(
            IDeEn? entity,
            string ISOCode639 = "de")
        {
            try
            {
                entity = CheckForNull(entity);

                switch (ISOCode639.ToLower())
                {
                    case "de":
                    case "deu":
                    case "ger":
                        return CheckForNull(entity.DE);
                    case "en":
                    case "eng":
                        return CheckForNull(entity.EN);
                    default:
                        throw new Exception("Unknown code");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Problem with '{ISOCode639}'", ex);
            }
        }

        public static string Find(
            IDeEnFr? entity,
            string ISOCode639 = "de")
        {
            try
            {
                entity = CheckForNull(entity);

                switch (ISOCode639.ToLower())
                {
                    case "de":
                    case "deu":
                    case "ger":
                        return CheckForNull(entity.DE);
                    case "en":
                    case "eng":
                        return CheckForNull(entity.EN);
                    case "fr":
                    case "fra":
                    case "fre":
                        return CheckForNull(entity.FR);
                    default:
                        throw new Exception("Unknown code");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Problem with '{ISOCode639}'", ex);
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private static T CheckForNull<T>(
            T? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(
                    nameof(entity));

            return entity;
        }

        private static string CheckForNull(
            string? text)
        {
            if (text == null ||
                text.Length == 0)
                throw new Exception("Text is empty");

            return text;
        }
        #endregion
    }
}
