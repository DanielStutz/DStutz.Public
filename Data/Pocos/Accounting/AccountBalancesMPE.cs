using DStutz.System.Joiners;

using DStutz.Data.Efcos.Accounting;

// Version 1.1
namespace DStutz.Data.Pocos.Accounting
{
    public interface IAccountBalances
    {
        public int Pk1 { get; set; } // Account number
        public int Pk2 { get; set; } // Year
        public long? Balance0101 { get; set; }
        public long? Balance1231 { get; set; }
        public string? Remark { get; set; }
    }

    public class AccountBalancesMPE
        : IPoco<IAccountBalances>, IAccountBalances
    {
        #region Properties
        /***********************************************************/
        public int Pk1 { get; set; } // Account number
        public int Pk2 { get; set; } // Year
        public long? Balance0101 { get; set; }
        public long? Balance1231 { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code (keys)
        /***********************************************************/
        public int Number { get { return Pk1; } }
        public int Year { get { return Pk2; } }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return AccountBalancesMapper.New.Joiner(this);
        }

        public E Map<E>() where E : IAccountBalances, new()
        {
            return AccountBalancesMapper.New.Map<E>(this);
        }
        #endregion
    }
}
