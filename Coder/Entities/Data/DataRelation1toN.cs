namespace DStutz.Coder.Entities.Data;

public class DataRelation1toN : DataProperty<DataTypeList>
{
    #region Title
    /***********************************************************/
    public static string Title = "Relations 1:n (with default foreign key)";
    #endregion

    #region Properties
    /***********************************************************/
    private string ForeignKey { get; } = "Pk1";
    #endregion

    #region Constructors
    /***********************************************************/
    public DataRelation1toN(
        JsonRelation1toN property)
        : base(
              property,
              new DataTypeList(property, property.ListType))
    {
        if (property.ListType.Contains("?"))
            throw new JsonOptionalException(
                "Relations1toN",
                "ListType");

        // List<CommentBLO>
        Type.AddBLO(Type.B);

        // List<CommentDAO>
        Type.AddDAO(Type.D);

        //Console.WriteLine("");
        //Console.WriteLine(GetProperty(Type.LB, Name, IsOptional));
        //Console.WriteLine(GetProperty(Type.LD, Name, IsOptional));
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    public override string[] GetPropertyBLO()
    {
        // public List<CommentBLO>? Comments { get; set; }
        return new string[] {
            GetSetProperty(Type.LB, Name, IsOptional),
        };
    }

    public override string[] GetPropertyDAO()
    {
        // [ForeignKey("Pk1")]
        // public List<CommentDAO>? Comments { get; set; }
        return new string[] {
            $"[ForeignKey(\"{ForeignKey}\")]",
            GetSetProperty(Type.LD, Name, IsOptional),
            "",
        };
    }
    #endregion
}
