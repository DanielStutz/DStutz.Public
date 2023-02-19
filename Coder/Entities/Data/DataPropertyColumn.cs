namespace DStutz.Coder.Entities.Data
{
    public class DataPropertyColumn : DataProperty<DataType>
    {
        #region Title
        /***********************************************************/
        public static string Title = "Properties";
        #endregion

        #region Properties
        /***********************************************************/
        private string ColumnAnnotation { get; }
        private char Align { get; }
        private int Width { get; }
        public string? Pseudonym { get; }
        public bool IsOrderBy { get; } = false;
        #endregion

        #region Constructors
        /***********************************************************/
        public DataPropertyColumn(
            JsonKey key)
            : base(
                  "OrderBy",
                  key,
                  new DataType(key))
        {
            ColumnAnnotation = $"[Column(\"order_by\"), Key]";
            Align = key.Align;
            Width = key.Width;
            IsOrderBy = true;
        }

        public DataPropertyColumn(
            JsonKey key,
            int number)
            : base(
                  "Pk" + number,
                  key,
                  new DataType(key))
        {
            ColumnAnnotation = $"[Column(\"pk{number}\"), Key]";
            Align = key.Align;
            Width = key.Width;
            Pseudonym = key.Pseudonym;
        }

        public DataPropertyColumn(
            JsonProperty property)
            : base(
                  property,
                  new DataType(property))
        {
            ColumnAnnotation = property.ColumnAnnotation;
            Align = property.Align;
            Width = property.Width;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public string[] GetJoin()
        {
            return new string[] {
                $"('{Align}', {Width}, e1.{Name})",
            };
        }

        public override string[] GetPropertyEfco()
        {
            return new string[] {
                ColumnAnnotation,
                GetSetProperty(Type.E, Name, IsOptional),
                "",
            };
        }

        public string[] GetPropertyAsymmetricKey()
        {
            return new string[] {
                GetProperty(Type.N, Pseudonym!, Name),
            };
        }

        protected string GetProperty(
            string type,
            string name,
            string data)
        {
            return $"public {type} {name} {{ get {{ return {data}; }} }}";
        }
        #endregion
    }
}
