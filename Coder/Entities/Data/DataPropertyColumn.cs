namespace DStutz.Coder.Entities.Data
{
    public class DataPropertyColumn : DataProperty
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
        #endregion

        #region Constructors
        /***********************************************************/
        public DataPropertyColumn(
            string name,
            string type,
            string column,
            char align,
            int width)
            : base(name, type)
        {
            ColumnAnnotation = $"[Column(\"{column}\"), Key]";
            Align = align;
            Width = width;
        }

        public DataPropertyColumn(
            JsonProperty property)
            : base(property)
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
                GetProperty(Type.E, Name, IsOptional),
                "",
            };
        }
        #endregion
    }

    public class Key : DataPropertyColumn
    {
        public Key(int number, string type)
            : base("Pk" + number, type, "pk" + number, 'L', 20)
        { }
    }

    public class OrderBy : DataPropertyColumn
    {
        public OrderBy()
            : base("OrderBy", "int", "order_by", 'R', 3)
        { }
    }
}
