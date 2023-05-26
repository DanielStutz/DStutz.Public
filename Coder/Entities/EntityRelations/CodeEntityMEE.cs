using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityRelations
{
    public class CodeEntityMEE : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
TABLE
public ABSTRACT class TYPE_EFCO
    : IEfco<TYPE_POCO>, TYPE_INTERFACE
{
CURSOR_PROPERTIES
CURSOR_RELATIONS
CURSOR_ASYMMETRIC_CODE

    #region Properties and methods implementing
    /***********************************************************/
    public IJoiner Joiner
    {
        get { return TYPE_MAPPER.New.Joiner(JOIN); }
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
        public CodeEntityMEE(
            DataEntityRelations entity)
            : base(Template)
        {
            if (entity.AbstractDAO)
            {
                Replace("TABLE", "");
                Replace("ABSTRACT", "abstract");
            }
            else
            {
                Replace("TABLE", entity.TableAnnotation);
                Replace("ABSTRACT ", "");
            }

            Replace("JOIN", entity.GetJoin());

            if (entity.Code.Asymmetric)
                InsertRegionAsymmetricCode(4);

            // Simple and owned properties
            SetCursor("PROPERTIES", 4)
                .InsertRegion(
                    DataPropertyColumn.Title,
                    entity.Properties,
                    e => e.GetPropertyDAO())
                .InsertRegion(
                    DataPropertyOwned.Title,
                    entity.OwnedProperties,
                    e => e.GetPropertyDAO());

            // Relation properties
            SetCursor("RELATIONS", 4)
                .InsertRegion(
                    DataRelation1to1.Title,
                    entity.Relations1to1,
                    e => e.GetPropertyDAO())
                .InsertRegion(
                    DataRelation1toN.Title,
                    entity.Relations1toN,
                    e => e.GetPropertyDAO())
                .InsertRegion(
                    DataRelationMto1.Title,
                    entity.RelationsMto1,
                    e => e.GetPropertyDAO())
                .InsertRegion(
                    DataRelationMtoN.Title,
                    entity.RelationsMtoN,
                    e => e.GetPropertyDAO());

            //Write(false, false);
        }
        #endregion
    }
}
