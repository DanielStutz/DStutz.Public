using DStutz.Data.Efcos.Accounting;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IAccountExtended
    {
        public int Pk1 { get; set; } // Account number
        public string Abbr { get; set; }
        public string Name { get; set; }
        public int? Contra0101 { get; set; }
        public int? Contra1231 { get; set; }
        public string? ContraText { get; set; }
    }

    public class AccountExtendedMPE
        : IPoco<IAccountExtended>, IAccountExtended
    {
        #region Properties
        /***********************************************************/
        public int Pk1 { get; set; } // Account number
        public string Abbr { get; set; }
        public string Name { get; set; }
        public int? Contra0101 { get; set; }
        public int? Contra1231 { get; set; }
        public string? ContraText { get; set; }
        #endregion

        #region Asymmetric code (keys)
        /***********************************************************/
        public int Number { get { return Pk1; } }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AccountExtendedMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IAccountExtended, new()
        {
            return AccountExtendedMapper.New.Map<E>(this);
        }
        #endregion
    }
}
