using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityRelations
{
    public class CodeEfco : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
TABLE
public class TYPE_EFCO
    : IEfco<TYPE_POCO>, TYPE_INTERFACE
{
CURSOR_PROPERTIES
CURSOR_RELATIONS
CURSOR_ASYMMETRIC_CODE

    #region Methods implementing
    /***********************************************************/
    public IJoiner Joiner()
    {
        return TYPE_MAPPER.New.Joiner(JOIN);
    }

    public TYPE_POCO Map()
    {
        return TYPE_MAPPER.New.Map<TYPE_POCO>(this);
    }
    #endregion
}";
        #endregion

        #region Constructors
        /***********************************************************/
        public CodeEfco(
            DataEntityRelations entity)
            : base(Template)
        {
            Replace("TABLE", entity.TableAnnotation);
            Replace("JOIN", entity.GetJoin());

            if (entity.Code.Asymmetric)
                InsertRegionAsymmetricCode(4);

            // Simple and owned properties
            SetCursor("PROPERTIES", 4)
                .InsertRegion(
                    DataPropertyColumn.Title,
                    entity.Properties,
                    e => e.GetPropertyEfco())
                .InsertRegion(
                    DataPropertyOwned.Title,
                    entity.OwnedProperties,
                    e => e.GetPropertyEfco());

            // Relation properties
            SetCursor("RELATIONS", 4)
                .InsertRegion(
                    DataRelation1to1.Title,
                    entity.Relations1to1,
                    e => e.GetPropertyEfco())
                .InsertRegion(
                    DataRelation1toN.Title,
                    entity.Relations1toN,
                    e => e.GetPropertyEfco())
                .InsertRegion(
                    DataRelationMto1.Title,
                    entity.RelationsMto1,
                    e => e.GetPropertyEfco())
                .InsertRegion(
                    DataRelationMtoN.Title,
                    entity.RelationsMtoN,
                    e => e.GetPropertyEfco());

            //Write(false, false);
        }
        #endregion
    }
}
