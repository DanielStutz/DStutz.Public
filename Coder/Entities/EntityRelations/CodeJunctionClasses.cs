using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityRelations
{
    public class CodeJunctionClasses : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
CURSOR_CLASSES
";
        #endregion

        #region Constructors
        /***********************************************************/
        public CodeJunctionClasses(
            DataEntityRelations data)
            : base(Template)
        {
            // Junction table classes
            SetCursor("CLASSES")
                .Insert(
                    data.RelationsMtoN,
                    e => e.GetJunctionTableClass());

            //Write();
        }
        #endregion
    }
}
