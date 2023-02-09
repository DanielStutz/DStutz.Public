using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityOwned
{
    public class CodeEfco : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
public class TYPE_EFCO
    : IEfco<TYPE_POCO>, TYPE_INTERFACE
{
CURSOR_PROPERTIES

    #region Methods implementing
    /***********************************************************/
    public IJoiner Joiner()
    {
        return TYPE_MAPPER.New.Joiner(this);
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
            DataEntityOwned entity)
            : base(Template)
        {
            // Typed properties
            SetCursor("PROPERTIES", 4)
                .InsertRegion(
                    DataPropertyColumn.Title,
                    entity.Properties,
                    e => e.GetPropertyEfco());

            //Write(false, false);
        }
    }
    #endregion
}
