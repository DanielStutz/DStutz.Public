using static DStutz.Coder.Entities.Data.DataType;

namespace DStutz.Coder.Entities.Data
{
    public class DataEntityOwned : DataEntity
    {
        #region Constructors
        /***********************************************************/
        public DataEntityOwned(
            JsonEntity entity)
            : base(entity, MEO, MPO)
        {
            foreach (var item in entity.Properties)
                Properties.Add(new DataPropertyColumn(item));
        }
        #endregion
    }
}
