namespace DStutz.Coder.Entities.Data;

public class JsonKey
{
    #region Used by all keys
    /***********************************************************/
    public string Type { get; set; }
    public string? Comment { get; set; }
    public char Align { get; set; } = 'R';
    public int Width { get; set; } = 20;
    public bool IsOrderBy { get { return Type.Equals("OrderBy"); } }
    #endregion

    #region Used by keys with a pseudonym only
    /***********************************************************/
    public string? Pseudonym { get; set; }
    #endregion
}
