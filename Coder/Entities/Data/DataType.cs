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
        public string I { get; set; } // Interface
        public string M { get; set; } // Mapper
        #endregion

        #region Constructors
        /***********************************************************/
        // Used by key properties
        public DataType(
            string name)
        {
            N = name.Replace("?", "");
            E = N;
            P = N;
            CheckName();
        }

        // Used by most other properties
        public DataType(
            JsonProperty property)
        {
            var type = property.Type.Replace("?", "");
            N = type.RemoveEnding("M_E", "M_O", "_E", "_O");
            E = type.Replace("_", "E");
            P = type.Replace("_", "P");
            CheckName();
        }

        // Used by m:n relations for owner and related types
        public DataType(
            JsonRelationMtoN property)
            : this((JsonProperty)property)
        {
            I = "I" + N;
        }

        // Used by m:n relations for junction types
        public DataType(
            DataType owner,
            DataType related,
            JsonRelationMtoN property)
        {
            // Make 'Owner' and 'Related' to 'OwnerRelatedRel'
            N = owner.N + related.N + "Rel";

            // E.g. RelEEAny and RelPEAny
            E = property.JunctionType.Replace("_", "E");
            P = property.JunctionType.Replace("_", "P");
        }

        // Used by entities
        public DataType(
            JsonEntity entity,
            string suffixEfco,
            string suffixPoco)
        {
            N = entity.Name;
            E = N + suffixEfco;
            P = N + suffixPoco;
            I = "I" + N;
            M = N + "Mapper";
            CheckName();
        }
        #endregion

        #region Properties combining
        /***********************************************************/
        public string PI
        {
            get { return $"{P}, {I}"; }
        }

        public string EPI
        {
            get { return $"{E}, {P}, {I}"; }
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return new Joiner(
                (30, N),
                (33, P),
                (33, E),
                (31, I),
                (36, M)
            );
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
