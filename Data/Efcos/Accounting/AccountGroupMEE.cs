using DStutz.System.Joiners;

using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1
namespace DStutz.Data.Efcos.Accounting
{
    [Table("account_group")]
    public class AccountGroupMEE
        : IEfco<AccountGroupMPE>, IAccountGroup
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public int Pk1 { get; set; } // Account class

        [Column("pk2"), Key]
        public int Pk2 { get; set; } // Account group 1

        [Column("pk3"), Key]
        public int Pk3 { get; set; } // Account group 2

        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return AccountGroupMapper.New.Joiner(this);
        }

        public AccountGroupMPE Map()
        {
            return AccountGroupMapper.New.Map<AccountGroupMPE>(this);
        }
        #endregion
    }

    public class AccountGroupMapper
        : IMapper<IAccountGroup>
    {
        public static AccountGroupMapper New { get; } = new AccountGroupMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IAccountGroup e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 1, e1.Pk1),
                ('R', 2, e1.Pk2),
                ('R', 3, e1.Pk3),
                ('L', 80, e1.Name)
            ).Add(data);
        }

        public E Map<E>(
            IAccountGroup e1) where E : IAccountGroup, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Pk2 = e1.Pk2,
                Pk3 = e1.Pk3,
                Name = e1.Name,
            };
        }
        #endregion
    }
}
