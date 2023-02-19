using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data
{
    public class JsonProperty
    {
        #region Used by all properties
        /***********************************************************/
        public string Name { get; set; }
        public string? Comment { get; set; }
        public string Type { get; set; }
        public string? Column { get; set; } // Use a name or 'NotMapped'
        public bool IsOptional { get { return Type.EndsWith("?"); } }
        #endregion

        #region Used by joinable properties only
        /***********************************************************/
        public char Align { get; set; } = 'L';
        public int Width { get; set; } = 20;
        #endregion

        #region Used by m:1 relation properties only
        /***********************************************************/
        public bool AddNavigationProperty { get; set; } = true;
        #endregion

        #region Property column annotation
        /***********************************************************/
        public string ColumnAnnotation
        {
            get
            {
                if (Column == null)
                    return $"[Column(\"{Name.ColumnName()}\")]";

                if (Column.Equals("NotMapped"))
                    return "[NotMapped]";

                return $"[Column(\"{Column}\")]";
            }
        }
        #endregion
    }
}
