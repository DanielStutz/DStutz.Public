namespace DStutz.Data;

// See https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes

// Delete this class together with PolyglotText

[Obsolete("See DStutz.System.Polyglot", false)]
public abstract class ISOCode639
{
    public static string Simplify(
        string ISOCode639x)
    {
        switch (ISOCode639x.ToLower())
        {
            case "de":
            case "deu":
            case "ger":
                return "de";
            case "en":
            case "eng":
                return "en";
            case "es":
            case "spa":
                return "es";
            case "fr":
            case "fra":
            case "fre":
                return "fr";
            case "it":
            case "ita":
                return "it";
            default:
                throw new Exception(
                    $"Code '{ISOCode639x}' unknown");
        }
    }
}
