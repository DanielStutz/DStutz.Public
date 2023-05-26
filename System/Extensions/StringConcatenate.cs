using System.Text;

namespace DStutz.System.Extensions;

public static class StringConcatenate
{
    #region Methods appending a string
    /***********************************************************/
    public static string Append(
        this string? self,
        string separator,
        object? other)
    {
        var sb = new StringBuilder(self ?? "");

        Append(sb, separator, other);

        return sb.ToString();
    }

    public static string Append(
        this string? self,
        string separator,
        params object?[] others)
    {
        var sb = new StringBuilder(self ?? "");

        foreach (var other in others)
            Append(sb, separator, other);

        return sb.ToString();
    }
    #endregion

    #region Methods prepending a string
    /***********************************************************/
    public static string Prepend(
        this string? self,
        string separator,
        object? other)
    {
        var sb = new StringBuilder(self ?? "");

        Prepend(sb, separator, other);

        return sb.ToString();
    }

    public static string Prepend(
        this string? self,
        string separator,
        params object?[] others)
    {
        var sb = new StringBuilder(self ?? "");

        foreach (var other in others)
            Prepend(sb, separator, other);

        return sb.ToString();
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    private static void Append(
        StringBuilder sb,
        string separator,
        object? value)
    {
        if (value != null)
            Append(sb, separator, value.ToString());
    }

    private static void Append(
        StringBuilder sb,
        string separator,
        string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            sb.Append($"{separator}{value}");
    }

    private static void Prepend(
        StringBuilder sb,
        string separator,
        object? value)
    {
        if (value != null)
            Prepend(sb, separator, value.ToString());
    }

    private static void Prepend(
        StringBuilder sb,
        string separator,
        string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            sb.Insert(0, $"{value}{separator}");
    }
    #endregion
}
