namespace DStutz.Coder.Entities.Data;

public class DataEntityRelations : DataEntityBasic
{
    #region Properties
    /***********************************************************/
    public List<DataPropertyOwned> OwnedProperties { get; } = new();
    public List<DataRelation1to1> Relations1to1 { get; } = new();
    public List<DataRelation1toN> Relations1toN { get; } = new();
    public List<DataRelationMto1> RelationsMto1 { get; } = new();
    public List<DataRelationMtoN> RelationsMtoN { get; } = new();
    #endregion

    #region Constructors
    /***********************************************************/
    public DataEntityRelations(
        JsonEntity entity)
        : base(entity)
    {
        if (entity.OwnedProperties != null)
            foreach (var item in entity.OwnedProperties)
                OwnedProperties.Add(new DataPropertyOwned(item));

        if (entity.Relations1to1 != null)
            foreach (var item in entity.Relations1to1)
                Relations1to1.Add(new DataRelation1to1(item));

        if (entity.Relations1toN != null)
            foreach (var item in entity.Relations1toN)
                Relations1toN.Add(new DataRelation1toN(item));

        if (entity.RelationsMto1 != null)
            foreach (var item in entity.RelationsMto1)
                RelationsMto1.Add(new DataRelationMto1(item));

        if (entity.RelationsMtoN != null)
            foreach (var item in entity.RelationsMtoN)
                RelationsMtoN.Add(new DataRelationMtoN(Type, item));
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    public string GetJoin()
    {
        var join = "this";

        foreach (var p in OwnedProperties)
            join += ", " + p.Name;

        return join;
    }
    #endregion
}
