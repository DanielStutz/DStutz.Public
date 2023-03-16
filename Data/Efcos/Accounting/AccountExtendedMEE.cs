using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("account_extended")]
    public class AccountExtendedMEE
        : IEfco<AccountExtendedMPE>, IAccountExtended
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public int Pk1 { get; set; } // Account number

        [Column("abbr")]
        public string Abbr { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("contra_0101")]
        public int? Contra0101 { get; set; }

        [Column("contra_1231")]
        public int? Contra1231 { get; set; }

        [Column("contra_text")]
        public string? ContraText { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AccountExtendedMapper.New.Joiner(this); }
        }

        public AccountExtendedMPE Map()
        {
            return AccountExtendedMapper.New.Map<AccountExtendedMPE>(this);
        }
        #endregion
    }

    public class AccountExtendedMapper
        : IMapper<IAccountExtended>
    {
        public static AccountExtendedMapper New { get; } = new AccountExtendedMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IAccountExtended e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 4, e1.Pk1),
                ('L', 10, e1.Abbr),
                ('L', 80, e1.Name),
                ('L', 4, e1.Contra0101),
                ('L', 4, e1.Contra1231),
                ('L', 80, e1.ContraText)
            ).Add(data);
        }

        public E Map<E>(
            IAccountExtended e1) where E : IAccountExtended, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Abbr = e1.Abbr,
                Name = e1.Name,
                Contra0101 = e1.Contra0101,
                Contra1231 = e1.Contra1231,
                ContraText = e1.ContraText,
            };
        }
        #endregion
    }
}
