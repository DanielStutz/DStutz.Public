using DStutz.System.Extensions;

namespace DStutz.Coder.Entities.Data
{
    public class DataType : IJoinable
    {
        public static string MEE { get; } = "MEE";
        public static string MPE { get; } = "MPE";
        public static string MEO { get; } = "MEO";
        public static string MPO { get; } = "MPO";

        public static string Name<T>()
        {
            return typeof(T).Name.Replace(MEE, "").Replace(MPE, "");
        }

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
            N = type.RemoveEnding("M_E", "M_O", "_E", "_O", "_");
            E = type.Replace("_", "E");
            P = type.Replace("_", "P");
            CheckName();
        }

        public DataType(
            DataType owner,
            DataType related,
            string junctionType)
        {
            // OwnerRelatedRel, RelEEAny and RelPEAny
            N = owner.N + related.N + "Rel";
            E = junctionType.Replace("_", "E");
            P = junctionType.Replace("_", "P");
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
