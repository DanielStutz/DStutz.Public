using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities
{
    public class CodeInterface : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
public interface TYPE_INTERFACE
    IMPLEMENTING
{
CURSOR_PROPERTIES
}";
        #endregion

        #region Constructors
        /***********************************************************/
        private CodeInterface(
            DataEntity entity,
            bool isOrderBy)
            : base(Template)
        {
            if (isOrderBy)
                Replace("IMPLEMENTING", ": IOrdered");
            else
                Replace("IMPLEMENTING", "");

            // Simple properties
            SetCursor("PROPERTIES", 4)
                .Insert(
                    entity.Properties.Where(e => !e.IsOrderBy),
                    e => e.GetProperty());
        }

        public CodeInterface(
            DataEntityBasic entity)
            : this(entity, entity.IsOrderBy)
        {
            //Write(false, false);
        }

        public CodeInterface(
            DataEntityOwned entity)
            : this(entity, false)
        {
            //Write(false, false);
        }

        public CodeInterface(
            DataEntityRelations entity)
            : this(entity, entity.IsOrderBy)
        {
            // Specific foreign keys
            SetCursor("PROPERTIES", 4)
                .Insert(
                    entity.RelationsMto1,
                    e => e.GetProperty());

            //Write(false, false);
        }
        #endregion
    }
}
