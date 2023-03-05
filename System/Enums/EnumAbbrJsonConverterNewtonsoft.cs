using Newtonsoft.Json;

namespace DStutz.System.Enums
{
    public class EnumAbbrJsonConverterNewtonsoft<E>
        : JsonConverter<E>
        where E : IEnumString<E>, new()
    {
        #region Properties
        /***********************************************************/
        private static E Mapper { get; } = new E();
        #endregion

        #region Constructors
        /***********************************************************/
        public EnumAbbrJsonConverterNewtonsoft()
        { }
        #endregion

        #region Methods converting
        /***********************************************************/
        public override E ReadJson(
            JsonReader reader,
            Type objectType,
            E? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null)
                throw new ArgumentNullException("Abbr");

            return Mapper.Map((string)reader.Value);
        }

        public override void WriteJson(
            JsonWriter writer,
            E? value,
            JsonSerializer serializer)
        {
            if (value == null)
                throw new ArgumentNullException("Enum");

            writer.WriteValue(Mapper.Map(value));
        }
        #endregion
    }
}
