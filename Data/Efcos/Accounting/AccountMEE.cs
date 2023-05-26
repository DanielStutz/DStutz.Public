using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("account")]
    public class AccountMEE
        : IEfco<AccountMPE>, IAccount
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public int Pk1 { get; set; } // Account number

        [Column("name")]
        public string Name { get; set; }

        [Column("group_3")]
        public int Group3 { get; set; }

        [Column("group_2")]
        public int Group2 { get; set; }

        [Column("class_1")]
        public int Class1 { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AccountMapper.New.Joiner(this); }
        }

        public AccountMPE Map()
        {
            return AccountMapper.New.Map<AccountMPE>(this);
        }
        #endregion
    }

    public class AccountMapper
        : IMapper<IAccount>
    {
        public static AccountMapper New { get; } = new AccountMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IAccount e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 4, e1.Pk1),
                ('L', 80, e1.Name),
                ('R', 3, e1.Group3),
                ('R', 2, e1.Group2),
                ('R', 1, e1.Class1)
            ).Add(data);
        }

        public E Map<E>(
            IAccount e1) where E : IAccount, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Name = e1.Name,
                Group3 = e1.Group3,
                Group2 = e1.Group2,
                Class1 = e1.Class1,
            };
        }
        #endregion
    }
}
