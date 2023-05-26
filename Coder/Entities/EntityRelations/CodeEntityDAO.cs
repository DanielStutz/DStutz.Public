using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityRelations
{
    public class CodeEntityDAO: CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
TABLE
public ABSTRACT class TYPE_DAO
    : IEntityDAO<TYPE_INT>, TYPE_INT
{
CURSOR_PROPERTIES
CURSOR_RELATIONS
CURSOR_ASYMMETRIC_CODE


    #region Methods implementing
    /***********************************************************/
    public void Update(
        TYPE_INT entity)
    {
CURSOR_ASSIGNS
    }
    #endregion
}";
        #endregion

        #region Constructors
        /***********************************************************/
        public CodeEntityDAO(
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

            // Simple properties
            SetCursor("ASSIGNS", 8)
                .Insert(
                    entity.Properties,
                    e => e.GetAssign());

            //Write(false, false);
        }
        #endregion
    }
}
