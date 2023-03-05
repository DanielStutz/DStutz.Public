using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DStutz.System.Enums
{
    public class EnumAbbrValueConverter<E>
        : ValueConverter<E, string>
        where E : IEnumString<E>, new()
    {
        #region Properties
        /***********************************************************/
        private static E Mapper { get; } = new E();
        #endregion

        #region Constructors
        /***********************************************************/
        public EnumAbbrValueConverter()
            : base(
                v => Mapper.Map(v),
                v => Mapper.Map(v))
        { }
        #endregion
    }
}
