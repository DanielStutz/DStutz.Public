using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityRelations
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
        var e2 = new E()
        {
CURSOR_ASSIGNS
        };

        if (typeof(E) == typeof(TYPE_EFCO))
        {
            TYPE_POCO poco = (TYPE_POCO)(object)e1;
            TYPE_EFCO efco = (TYPE_EFCO)(object)e2;

CURSOR_MAPPING_P2E
        }
        else if (typeof(E) == typeof(TYPE_POCO))
        {
            TYPE_EFCO efco = (TYPE_EFCO)(object)e1;
            TYPE_POCO poco = (TYPE_POCO)(object)e2;

CURSOR_MAPPING_E2P
        }
        else
        {
            throw new NotImplementedException();
        }

        return e2;
    }
    #endregion
}";
        #endregion

        #region Constructors
        /***********************************************************/
        public CodeMapper(
            DataEntityRelations entity)
            : base(Template)
        {
            // Typed properties and specific foreign keys
            SetCursor("JOINS", 12)
                .SetAppend(",", "")
                .Insert(
                    entity.Properties,
                    e => e.GetJoin())
                .Insert(
                    entity.RelationsMto1,
                    e => e.GetJoin());

            // Typed properties and specific foreign keys
            SetCursor("ASSIGNS", 12)
                .Insert(
                    entity.Properties,
                    e => e.GetAssign())
                .Insert(
                    entity.RelationsMto1,
                    e => e.GetAssign());

            // Mapping
            SetCursor("MAPPING_P2E", 12)
                .Insert(
                    entity.OwnedProperties,
                    e => e.GetMappingP2E())
                .Insert(
                    entity.Relations1to1,
                    e => e.GetMappingP2E())
                .Insert(
                    entity.Relations1toN,
                    e => e.GetMappingP2E())
                .Insert(
                    entity.RelationsMto1,
                    e => e.GetMappingP2E())
                .Insert(
                    entity.RelationsMtoN,
                    e => e.GetMappingP2E());

            SetCursor("MAPPING_E2P", 12)
                .Insert(
                    entity.OwnedProperties,
                    e => e.GetMappingE2P())
                .Insert(
                    entity.Relations1to1,
                    e => e.GetMappingE2P())
                .Insert(
                    entity.Relations1toN,
                    e => e.GetMappingE2P())
                .Insert(
                    entity.RelationsMto1,
                    e => e.GetMappingE2P())
                .Insert(
                    entity.RelationsMtoN,
                    e => e.GetMappingE2P());

            //Write(false, false);
        }
        #endregion
    }
}
