using System.Text.Json;
using System.Text.Json.Serialization;

namespace DStutz.System.Enums
{
    public class EnumAbbrJsonConverter<E>
        : JsonConverter<E>
        where E : IEnumString<E>, new()
    {
        #region Properties
        /***********************************************************/
        private static E Mapper { get; } = new E();
        #endregion

        #region Constructors
        /***********************************************************/
        public EnumAbbrJsonConverter()
        { }
        #endregion

        #region Methods converting
        /***********************************************************/
        public override E Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (value == null)
                throw new ArgumentNullException("Abbr");

            return Mapper.Map(value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            E value,
            JsonSerializerOptions options)
        {
            if (value == null)
                throw new ArgumentNullException("Enum");

            writer.WriteStringValue(Mapper.Map(value));
        }
        #endregion
    }
}
