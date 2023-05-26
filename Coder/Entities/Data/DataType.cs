namespace DStutz.Coder.Entities.Data;

public class DataType : IJoinableNew
{
    private static string XYZ { get; } = "XYZ";
    public static string BLO { get; } = "BLO";
    public static string DAO { get; } = "DAO";
    public static string BLOO { get; } = "BLOO";
    public static string DAOO { get; } = "DAOO";

    public static string Name<T>()
    {
        return typeof(T).Name.Replace(BLO, "").Replace(DAO, "");
    }

    #region Properties
    /***********************************************************/
    public string N { get; set; } // Name
    public string B { get; set; } // BLO
    public string D { get; set; } // DAO
    public bool IsCollection { get; set; } = false;
    #endregion

    #region Constructors
    /***********************************************************/
    public DataType(
        string name)
    {
        N = name.Replace("?", "");
        B = N;
        D = N;
        CheckName();
    }

    public DataType(
        JsonKey key)
    {
        N = key.IsOrderBy ? "int" : key.Type;
        B = N;
        D = N;
        CheckName();
    }

    public DataType(
        JsonProperty property)
    {
        var type = property.Type.Replace("?", "");
        //N = type.RemoveEnding("M_E", "M_O", "_E", "_O", "_");
        N = type.Replace(XYZ, "");
        B = type.Replace(XYZ, BLO);
        D = type.Replace(XYZ, DAO);
        CheckName();
    }

    public DataType(
        DataType owner,
        DataType related,
        string junctionType)
    {
        // OwnerRelatedRel, RelEEAny and RelPEAny
        N = owner.N + related.N + "Rel";
        B = junctionType.Replace(XYZ, BLO);
        D = junctionType.Replace(XYZ, DAO);
    }
    #endregion

    #region Methods implementing
    /***********************************************************/
    public virtual IJoiner Joiner()
    {
        return new Joiner(
            (30, N),
            (33, B),
            (33, D)
        );
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    private void CheckName()
    {
        if (N.Contains("_"))
            throw new Exception(
                $"Name '{N}' has an incorrect ending");

        if (N.Contains("?"))
            throw new Exception(
                $"Name '{N}' has an incorrect char '?'");
    }
    #endregion
}
