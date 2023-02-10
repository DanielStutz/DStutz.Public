using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data
{
    public class DataRelationMto1 : DataProperty<DataType>
    {
        #region Title
        /***********************************************************/
        public static string Title = "Relations m:1 (with specific foreign key)";
        #endregion

        #region Properties
        /***********************************************************/
        private string? Column { get; }
        private string ForeignKey { get; } = "Pk1";
        private string ForeignKeyType { get; } = "long";
        public bool AddNavigationProperty { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public DataRelationMto1(
        JsonProperty property)
            : base(
                  property,
                  new DataType(property))
        {
            Column = property.Column;
            ForeignKey = Name + ForeignKey;

            // Unused at the moment, see below
            AddNavigationProperty = property.AddNavigationProperty;
        }
        #endregion

        #region Property column annotation
        /***********************************************************/
        private string ColumnAnnotation
        {
            get
            {
                if (Column == null)
                    return $"[Column(\"{Name.ColumnName()}_pk1\")]";

                return $"[Column(\"{Column}\")]";
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string[] GetAssign()
        {
            return new string[] {
                $"{ForeignKey} = e1.{ForeignKey},",
            };
        }

        public string[] GetJoin()
        {
            return new string[] {
                $"('L', 20, e1.{ForeignKey})",
            };
        }

        public override string[] GetProperty()
        {
            // public long? CategoryPk1 { get; set; }
            return new string[] {
                GetProperty(ForeignKeyType, ForeignKey, IsOptional),
            };
        }

        public override string[] GetPropertyEfco()
        {
            // [Column("category_pk1")]
            // public long? CategoryPk1 { get; set; }
            // [ForeignKey("CategoryPk1")]
            // public CategoryEE? Category { get; set; }
            var lines = new List<string> {
                ColumnAnnotation,
                GetProperty(ForeignKeyType, ForeignKey, IsOptional),
                "",
                $"[ForeignKey(\"{ForeignKey}\")]",
                GetProperty(Type.E, Name, IsOptional),
                "",
            };

            // Unused at the moment
            //if (!AddNavigationProperty)
            //    lines.RemoveRange(3, 3);

            return lines.ToArray();
        }

        public override string[] GetPropertyPoco()
        {
            // public long? CategoryPk1 { get; set; }
            // public CategoryPE? Category { get; set; }
            var lines = new List<string> {
                GetProperty(ForeignKeyType, ForeignKey, IsOptional),
                GetProperty(Type.P, Name, IsOptional),
                "",
            };

            // Unused at the moment
            //if (!AddNavigationProperty)
            //    lines.RemoveAt(1);

            return lines.ToArray();
        }
        #endregion
    }
}
