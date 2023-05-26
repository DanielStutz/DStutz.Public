namespace DStutz.Coder.Entities.Data;

public class DataRelation1to1 : DataProperty<DataType>
{
    #region Title
    /***********************************************************/
    public static string Title = "Relations 1:1 (with default foreign key)";
    #endregion

    #region Properties
    /***********************************************************/
    private string ForeignKey { get; } = "Pk1";
    #endregion

    #region Constructors
    /***********************************************************/
    public DataRelation1to1(
        JsonProperty property)
        : base(
              property,
              new DataType(property))
    { }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    public override string[] GetPropertyBLO()
    {
        // public NameBLO? Name { get; set; }
        return new string[] {
            GetSetProperty(Type.B, Name, IsOptional),
        };
    }

    public override string[] GetPropertyDAO()
    {
        // [ForeignKey("Pk1")]
        // public NameDAO? Name { get; set; }
        return new string[] {
            $"[ForeignKey(\"{ForeignKey}\")]",
            GetSetProperty(Type.D, Name, IsOptional),
            "",
        };
    }
    #endregion
}
