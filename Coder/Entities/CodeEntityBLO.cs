using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities;

public class CodeEntityBLO : CodeBlock
{
    #region Template
    /***********************************************************/
    private static readonly string Template = @"
public ABSTRACT class TYPE_BLO
    : IEntityBLO<TYPE_INT>, TYPE_INT
{
CURSOR_PROPERTIES
CURSOR_RELATIONS
CURSOR_ASYMMETRIC_CODE

    #region Methods implementing
    /***********************************************************/
    public object[] Keys()
    {
        return new object[] { KEYS };
    }

    public void Update(
        TYPE_INT entity)
    {
CURSOR_ASSIGNS
    }

    public IJoiner Joiner()
    {
        return new Joiner(
            //('L', 20, GetType().Name),
CURSOR_JOINS
        );
    }
    #endregion
}";
    #endregion

    #region Constructors
    /***********************************************************/
    private CodeEntityBLO(
        DataEntity entity,
        bool isAbstract,
        string keys,
        string join)
        : base(Template)
    {
        if (isAbstract)
            Replace("ABSTRACT", "abstract");
        else
            Replace("ABSTRACT ", "");

        Replace("KEYS", keys);
        //Replace("JOIN", join);

        if (entity.Code.Asymmetric)
            InsertRegionAsymmetricCode(4);

        // Simple properties and keys with a pseudonym
        SetCursor("PROPERTIES", 4)
            .InsertRegion(
                DataPropertyColumn.Title,
                entity.Properties,
                e => e.GetProperty())
            .InsertRegion(
                CodeHelper.AsymmetricKeys,
                entity.Properties.Where(e => e.Pseudonym != null),
                e => e.GetPropertyAsymmetricKey());

        // Simple properties
        SetCursor("ASSIGNS", 8)
            .Insert(
                entity.Properties,
                e => e.GetAssign());

        // Simple properties
        SetCursor("JOINS", 12)
            .SetAppend(",", "")
            .Insert(
                entity.Properties,
                e => e.GetJoin());
    }

    public CodeEntityBLO(
        DataEntityBasic entity)
        : this(
              entity,
              entity.AbstractBLO,
              entity.Keys,
              "this")
    {
        //Write(false, false);
    }

    public CodeEntityBLO(
        DataEntityOwned entity)
        : this(
              entity,
              false,
              "",
              "this")
    {
        //Write(false, false);
    }

    public CodeEntityBLO(
        DataEntityRelations entity)
        : this(
              entity,
              entity.AbstractBLO,
              entity.Keys,
              entity.GetJoin())
    {
        // Owned properties
        SetCursor("PROPERTIES", 4)
            .InsertRegion(
                DataPropertyOwned.Title,
                entity.OwnedProperties,
                e => e.GetPropertyBLO());

        // Relation properties
        SetCursor("RELATIONS", 4)
            .InsertRegion(
                DataRelation1to1.Title,
                entity.Relations1to1,
                e => e.GetPropertyBLO())
            .InsertRegion(
                DataRelation1toN.Title,
                entity.Relations1toN,
                e => e.GetPropertyBLO())
            .InsertRegion(
                DataRelationMto1.Title,
                entity.RelationsMto1,
                e => e.GetPropertyBLO())
            .InsertRegion(
                DataRelationMtoN.Title,
                entity.RelationsMtoN,
                e => e.GetPropertyBLO());

        //Write(false, false);
    }
    #endregion
}
