namespace DStutz.Coder.Entities.Data;

public abstract class DataEntity
{
    #region Properties of all entities
    /***********************************************************/
    public CodeInfoEntity Code { get; set; }
    public string Namespace { get; }
    public string Name { get; }
    public DataTypeEntity Type { get; }
    public List<DataPropertyColumn> Properties { get; } = new();
    #endregion

    #region Constructors
    /***********************************************************/
    protected DataEntity(
        JsonEntity entity,
        string suffixBLO,
        string suffixDAO)
    {
        Code = entity.Code;
        Namespace = entity.Namespace;
        Name = entity.Name;
        Type = new DataTypeEntity(entity, suffixBLO, suffixDAO);
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    public string GetNamespaceBLO()
    {
        return Namespace.Replace("#", DataType.BLO);
    }

    public string GetNamespaceDAO()
    {
        return Namespace.Replace("#", DataType.DAO);
    }

    public string GetNamespaceCRUD()
    {
        return Namespace.Replace("#", "Cruder");
    }
    #endregion
}
