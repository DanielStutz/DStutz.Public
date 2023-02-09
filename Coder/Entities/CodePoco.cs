using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities
{
    public class CodePoco : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
public class TYPE_POCO
    : IPoco<TYPE_INTERFACE>, TYPE_INTERFACE
{
CURSOR_PROPERTIES
CURSOR_RELATIONS

    #region Methods implementing
    /***********************************************************/
    public IJoiner Joiner()
    {
        return TYPE_MAPPER.New.Joiner(JOIN);
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
            DataEntity entity)
            : base(Template)
        {
            // Typed properties
            SetCursor("PROPERTIES", 4)
                .InsertRegion(
                    DataPropertyColumn.Title,
                    entity.Properties,
                    e => e.GetProperty());
        }

        public CodePoco(
            DataEntityBasic entity)
            : this((DataEntity)entity)
        {
            Replace("JOIN", "this");

            //Write();
        }

        public CodePoco(
            DataEntityOwned entity)
            : this((DataEntity)entity)
        {
            Replace("JOIN", "this");

            //Write();
        }

        public CodePoco(
            DataEntityRelations entity)
            : this((DataEntity)entity)
        {
            Replace("JOIN", entity.GetJoin());

            // Owned properties
            SetCursor("PROPERTIES", 4)
                .Insert(
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

            //Write();
        }
        #endregion
    }
}
