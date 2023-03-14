using DStutz.System.Joiners;

using DStutz.Data.Efcos.Accounting;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IAccount
    {
        public int Pk1 { get; set; } // Account number
        public string Name { get; set; }
        public int Group3 { get; set; }
        public int Group2 { get; set; }
        public int Class1 { get; set; }
    }

    public class AccountMPE
        : IPoco<IAccount>, IAccount
    {
        #region Properties
        /***********************************************************/
        public int Pk1 { get; set; } // Account number
        public string Name { get; set; }
        public int Group3 { get; set; }
        public int Group2 { get; set; }
        public int Class1 { get; set; }
        #endregion

        #region Asymmetric code (keys)
        /***********************************************************/
        public int Number { get { return Pk1; } }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AccountMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IAccount, new()
        {
            return AccountMapper.New.Map<E>(this);
        }
        #endregion
    }
}
