namespace DStutz.Coder.Entities.Data;

public class JsonRelationMtoN : JsonProperty
{
    #region Used by m:n relation properties
    /***********************************************************/
    public string ListType { get; set; } = "IReadOnlyList";
    public string? JunctionTable { get; set; }
    public string JunctionType { get; set; }
    #endregion
}
