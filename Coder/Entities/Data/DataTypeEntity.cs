using DStutz.System.Joiners;

namespace DStutz.Coder.Entities.Data
{
    public class DataTypeEntity : DataType
    {
        #region Properties
        /***********************************************************/
        public string I { get; } // Interface
        public string M { get; } // Mapper
        #endregion

        #region Constructors
        /***********************************************************/
        public DataTypeEntity(
            JsonEntity entity,
            string suffixEfco,
            string suffixPoco)
            : base(entity.Name)
        {
            E = N + suffixEfco;
            P = N + suffixPoco;
            I = "I" + N;
            M = N + "Mapper";
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public override IJoiner Joiner()
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
    }
}
