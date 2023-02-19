namespace DStutz.Coder.Entities.Data
{
    public class DataPropertyOwned : DataProperty<DataType>
    {
        #region Title
        /***********************************************************/
        public static string Title = "Properties owned";
        #endregion

        #region Constructors
        /***********************************************************/
        public DataPropertyOwned(
            JsonProperty property)
            : base(
                  property,
                  new DataType(property.Type))
        { }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string[] GetPropertyEfco()
        {
            return new string[] {
                "// Owned",
                GetSetProperty(Type.E, Name, IsOptional),
                "",
            };
        }
        #endregion
    }
}
