using static DStutz.Coder.Entities.Data.DataType;

namespace DStutz.Coder.Entities.Data
{
    public class DataEntityBasic : DataEntity
    {
        #region Properties
        /***********************************************************/
        public bool AbstractEfco { get; } = false;
        public bool AbstractPoco { get; } = false;
        public bool IsOrderBy { get; }
        public string TableAnnotation { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public DataEntityBasic(
            JsonEntity entity)
        : base(entity, MEE, MPE)
        {
            if (entity.Abstract != null)
            {
                AbstractEfco = entity.Abstract.Contains("E");
                AbstractPoco = entity.Abstract.Contains("P");
            }

            IsOrderBy = entity.HasOrderByKey;
            TableAnnotation = entity.TableAnnotation;

            int i = 1;

            if (entity.Keys != null)
                foreach (var key in entity.Keys)
                    if (key.IsOrderBy)
                        Properties.Add(
                            new DataPropertyColumn(key));
                    else
                        Properties.Add(
                            new DataPropertyColumn(key, i++));

            if (entity.Properties != null)
                foreach (var property in entity.Properties)
                    Properties.Add(
                        new DataPropertyColumn(property));
        }
        #endregion
    }
}
