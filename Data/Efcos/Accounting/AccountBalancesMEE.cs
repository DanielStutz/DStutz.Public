using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("account_balances")]
    public class AccountBalancesMEE
        : IEfco<AccountBalancesMPE>, IAccountBalances
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public int Pk1 { get; set; } // Account number

        [Column("pk2"), Key]
        public int Pk2 { get; set; } // Year

        [Column("balance_0101")]
        public long? Balance0101 { get; set; }

        [Column("balance_1231")]
        public long? Balance1231 { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AccountBalancesMapper.New.Joiner(this); }
        }

        public AccountBalancesMPE Map()
        {
            return AccountBalancesMapper.New.Map<AccountBalancesMPE>(this);
        }
        #endregion
    }

    public class AccountBalancesMapper
        : IMapper<IAccountBalances>
    {
        public static AccountBalancesMapper New { get; } = new AccountBalancesMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IAccountBalances e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 4, e1.Pk1),
                ('R', 4, e1.Pk2),
                ('R', 10, e1.Balance0101),
                ('R', 10, e1.Balance1231),
                ('L', 40, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            IAccountBalances e1) where E : IAccountBalances, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Pk2 = e1.Pk2,
                Balance0101 = e1.Balance0101,
                Balance1231 = e1.Balance1231,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
