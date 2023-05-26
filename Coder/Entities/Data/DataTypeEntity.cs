namespace DStutz.Coder.Entities.Data;

public class DataTypeEntity : DataType
{
    #region Properties
    /***********************************************************/
    public string I { get; } // Interface
    public string C { get; } // Cruder
    #endregion

    #region Constructors
    /***********************************************************/
    public DataTypeEntity(
        JsonEntity entity,
        string suffixBLO,
        string suffixDAO)
        : base(entity.Name)
    {
        B = N + suffixBLO;
        D = N + suffixDAO;
        I = "I" + N;
        C = N + "Cruder";
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
            (31, I),
            (36, C)
        );
    }
    #endregion
}
