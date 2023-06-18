namespace DStutz.Data;

[Obsolete("See DStutz.System.Polyglot", false)]
public abstract class PolyglotText
{
    #region Methods
    /***********************************************************/
    public static string Find(
        IDeEnOLD? entity,
        string ISOCode639x = "de")
    {
        try
        {
            entity = CheckForNull(entity);

            switch (ISOCode639.Simplify(ISOCode639x))
            {
                case "de":
                    return CheckForNull(entity.DE);
                case "en":
                    return CheckForNull(entity.EN);
                default:
                    throw new Exception("Unknown code");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Problem with '{ISOCode639x}'", ex);
        }
    }

    public static string Find(
        IDeEnFrOLD? entity,
        string ISOCode639x = "de")
    {
        try
        {
            entity = CheckForNull(entity);

            switch (ISOCode639.Simplify(ISOCode639x))
            {
                case "de":
                    return CheckForNull(entity.DE);
                case "en":
                    return CheckForNull(entity.EN);
                case "fr":
                    return CheckForNull(entity.FR);
                default:
                    throw new Exception("Unknown code");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Problem with '{ISOCode639x}'", ex);
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
