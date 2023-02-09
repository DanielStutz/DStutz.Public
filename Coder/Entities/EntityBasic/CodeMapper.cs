using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityBasic
{
    public class CodeMapper : CodeBlock
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
public class TYPE_MAPPER
    : IMapper<TYPE_INTERFACE>
{
    public static TYPE_MAPPER New { get; } = new TYPE_MAPPER();

    #region Methods implementing
    /***********************************************************/
    public IJoiner Joiner(
        TYPE_INTERFACE e1,
        params IJoinable?[] data)
    {
        return new Joiner(
            //('L', 20, e1.GetType().Name),
CURSOR_JOINS
        ).Add(data);
    }

    public E Map<E>(
        TYPE_INTERFACE e1) where E : TYPE_INTERFACE, new()
    {
        return new E()
        {
CURSOR_ASSIGNS
        };
    }
    #endregion
}";
        #endregion

        #region Constructors
        /***********************************************************/
        public CodeMapper(
            DataEntityBasic entity)
            : base(Template)
        {
            // Typed properties
            SetCursor("JOINS", 12)
                .SetAppend(",", "")
                .Insert(
                    entity.Properties,
                    e => e.GetJoin());

            // Typed properties
            SetCursor("ASSIGNS", 12)
                .Insert(
                    entity.Properties,
                    e => e.GetAssign());

            //Write(false, false);
        }
        #endregion
    }
}
