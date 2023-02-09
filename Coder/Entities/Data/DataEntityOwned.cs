namespace DStutz.Coder.Entities.Data
{
    public class DataEntityOwned : DataEntity
    {
        #region Constructors
        /***********************************************************/
        public DataEntityOwned(
            JsonEntity entity)
            : base(entity,
                  "MEO", "MPO") // See class DataType for endings
        {
            foreach (var item in entity.Properties)
                Properties.Add(new DataPropertyColumn(item));
        }
        #endregion
    }
}
