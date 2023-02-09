namespace DStutz.Coder.Entities.Data
{
    public class DataEntityBasic : DataEntity
    {
        #region Properties
        /***********************************************************/
        public bool Abstract { get; }
        public string TableAnnotation { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public DataEntityBasic(
            JsonEntity entity)
            : base(entity,
                  "MEE", "MPE") // See class DataType for endings
        {
            Abstract = entity.Abstract;
            TableAnnotation = entity.TableAnnotation;

            if (entity.Key1 != null)
                Properties.Add(new Key(1, entity.Key1));

            if (entity.Key2 != null)
                Properties.Add(new Key(2, entity.Key2));

            if (entity.Key3 != null)
                Properties.Add(new Key(3, entity.Key3));

            if (entity.OrderBy)
                Properties.Add(new OrderBy());

            if (entity.Properties != null)
                foreach (var item in entity.Properties)
                    Properties.Add(new DataPropertyColumn(item));
        }
        #endregion
    }
}
