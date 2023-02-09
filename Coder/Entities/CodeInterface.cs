using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities
{
    public class CodeInterface : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
public interface TYPE_INTERFACE
{
CURSOR_PROPERTIES
}";
        #endregion

        #region Constructors
        /***********************************************************/
        private CodeInterface(
            DataEntity entity)
            : base(Template)
        {
            // Typed properties
            SetCursor("PROPERTIES", 4)
                .Insert(
                    entity.Properties,
                    e => e.GetProperty());
        }

        public CodeInterface(
            DataEntityBasic entity)
            : this((DataEntity)entity)
        {
            //Write();
        }

        public CodeInterface(
            DataEntityOwned entity)
            : this((DataEntity)entity)
        {
            //Write();
        }

        public CodeInterface(
            DataEntityRelations entity)
            : this((DataEntity)entity)
        {
            // Specific foreign keys
            SetCursor("PROPERTIES", 4)
                .Insert(
                    entity.RelationsMto1,
                    e => e.GetProperty());

            //Write();
        }
        #endregion
    }
}
