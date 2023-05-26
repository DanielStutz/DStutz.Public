namespace DStutz.Coder.Entities.Data;

public class DataTypeList : DataType
{
    #region Properties
    /***********************************************************/
    public string I { get; } // Interface
    public string LB { get; set; } // List BLO
    public string LD { get; set; } // List DOA
    #endregion

    #region Constructors
    /***********************************************************/
    public DataTypeList(
        JsonProperty property,
        string listType)
        : base(property)
    {
        I = "I" + N;
        LD = listType;
        LB = listType;
        IsCollection = true;
    }
    #endregion

    #region Methods implementing
    /***********************************************************/
    public override IJoiner Joiner()
    {
        return new Joiner(
            (30, N),
            (33, B),
            (33, D),
            (31, I)
        );
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    public void AddBLO(
        string type)
    {
        LB += $"<{type}>";
    }

    public void AddDAO(
        string type)
    {
        LD += $"<{type}>";
    }

    public string BI
    {
        get { return $"{B}, {I}"; }
    }

    public string DBI
    {
        get { return $"{D}, {B}, {I}"; }
    }
    #endregion
}
