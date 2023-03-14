using DStutz.System.Joiners;

namespace DStutz.Coder.Entities.Data
{
    public class DataTypeList : DataType
    {
        #region Properties
        /***********************************************************/
        public string I { get; } // Interface
        public string LE { get; set; } // List efco
        public string LP { get; set; } // List poco
        #endregion

        #region Constructors
        /***********************************************************/
        public DataTypeList(
            JsonProperty property,
            string listType)
            : base(property)
        {
            I = "I" + N;
            LE = listType;
            LP = listType;
            IsCollection = true;
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public override IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    (30, N),
                    (33, P),
                    (33, E),
                    (31, I)
                );
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public void AddEfco(
            string type)
        {
            LE += $"<{type}>";
        }

        public void AddPoco(
            string type)
        {
            LP += $"<{type}>";
        }

        public string PI
        {
            get { return $"{P}, {I}"; }
        }

        public string EPI
        {
            get { return $"{E}, {P}, {I}"; }
        }
        #endregion
    }
}
