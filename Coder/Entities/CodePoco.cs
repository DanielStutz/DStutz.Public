using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities
{
    public class CodePoco : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
public ABSTRACT class TYPE_POCO
    : IPoco<TYPE_INTERFACE>, TYPE_INTERFACE
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

    public E Map<E>() where E : TYPE_INTERFACE, new()
    {
        return TYPE_MAPPER.New.Map<E>(this);
    }
    #endregion
}";
        #endregion

        #region Constructors
        /***********************************************************/
        private CodePoco(
            DataEntity entity,
            bool isAbstract,
            string join)
            : base(Template)
        {
            if (isAbstract)
                Replace("ABSTRACT", "abstract");
            else
                Replace("ABSTRACT ", "");

            Replace("JOIN", join);

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
        }

        public CodePoco(
            DataEntityBasic entity)
            : this(
                  entity,
                  entity.AbstractPoco,
                  "this")
        {
             //Write(false, false);
        }

        public CodePoco(
            DataEntityOwned entity)
            : this(
                  entity,
                  false,
                  "this")
        {
            //Write(false, false);
        }

        public CodePoco(
            DataEntityRelations entity)
            : this(
                  entity,
                  entity.AbstractPoco,
                  entity.GetJoin())
        {
            // Owned properties
            SetCursor("PROPERTIES", 4)
                .InsertRegion(
                    DataPropertyOwned.Title,
                    entity.OwnedProperties,
                    e => e.GetPropertyPoco());

            // Relation properties
            SetCursor("RELATIONS", 4)
                .InsertRegion(
                    DataRelation1to1.Title,
                    entity.Relations1to1,
                    e => e.GetPropertyPoco())
                .InsertRegion(
                    DataRelation1toN.Title,
                    entity.Relations1toN,
                    e => e.GetPropertyPoco())
                .InsertRegion(
                    DataRelationMto1.Title,
                    entity.RelationsMto1,
                    e => e.GetPropertyPoco())
                .InsertRegion(
                    DataRelationMtoN.Title,
                    entity.RelationsMtoN,
                    e => e.GetPropertyPoco());

            //Write(false, false);
        }
        #endregion
    }
}
