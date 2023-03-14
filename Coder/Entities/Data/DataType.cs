using DStutz.System.Extensions;
using DStutz.System.Joiners;

namespace DStutz.Coder.Entities.Data
{
    public class DataType : IJoinable
    {
        #region Properties
        /***********************************************************/
        public string N { get; set; } // Name
        public string E { get; set; } // Efco
        public string P { get; set; } // Poco
        public bool IsCollection { get; set; } = false;
        #endregion

        #region Constructors
        /***********************************************************/
        public DataType(
            string name)
        {
            N = name.Replace("?", "");
            E = N;
            P = N;
            CheckName();
        }

        public DataType(
            JsonKey key)
        {
            N = key.IsOrderBy ? "int" : key.Type;
            E = N;
            P = N;
            CheckName();
        }

        public DataType(
            JsonProperty property)
        {
            var type = property.Type.Replace("?", "");
            N = type.RemoveEnding("M_E", "M_O", "_E", "_O");
            E = type.Replace("_", "E");
            P = type.Replace("_", "P");
            CheckName();
        }

        public DataType(
            DataType owner,
            DataType related,
            string junctioType)
        {
            // OwnerRelatedRel, RelEEAny and RelPEAny
            N = owner.N + related.N + "Rel";
            E = junctioType.Replace("_", "E");
            P = junctioType.Replace("_", "P");
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public virtual IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    (30, N),
                    (33, P),
                    (33, E)
                );
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private void CheckName()
        {
            if (N.Contains("_"))
                throw new Exception(
                    $"Name '{N}' has an incorrect ending");

            if (N.Contains("?"))
                throw new Exception(
                    $"Name '{N}' has an incorrect char '?'");
        }
        #endregion
    }
}
