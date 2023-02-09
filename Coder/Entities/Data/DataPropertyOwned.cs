namespace DStutz.Coder.Entities.Data
{
    public class DataPropertyOwned : DataProperty
    {
        #region Title
        /***********************************************************/
        public static string Title = "Properties owned";
        #endregion

        #region Constructors
        /***********************************************************/
        public DataPropertyOwned(
            JsonProperty property)
            : base(property)
        { }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string[] GetPropertyEfco()
        {
            return new string[] {
                "// Owned",
                GetProperty(Type.E, Name, IsOptional),
                "",
            };
        }
        #endregion
    }
}
