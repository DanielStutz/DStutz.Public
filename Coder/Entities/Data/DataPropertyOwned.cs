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
                  new DataType(property))
        { }
        #endregion
    }
}
