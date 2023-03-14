using DStutz.System.Joiners;

using DStutz.Data.Efcos.Accounting;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IAccountGroup
    {
        public int Pk1 { get; set; } // Account class
        public int Pk2 { get; set; } // Account group 1
        public int Pk3 { get; set; } // Account group 2
        public string Name { get; set; }
    }

    public class AccountGroupMPE
        : IPoco<IAccountGroup>, IAccountGroup
    {
        #region Properties
        /***********************************************************/
        public int Pk1 { get; set; } // Account class
        public int Pk2 { get; set; } // Account group 1
        public int Pk3 { get; set; } // Account group 2
        public string Name { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AccountGroupMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IAccountGroup, new()
        {
            return AccountGroupMapper.New.Map<E>(this);
        }
        #endregion
    }
}
