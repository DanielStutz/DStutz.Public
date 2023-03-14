using DStutz.System.Joiners;

using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("voucher")]
    public class VoucherMEE
        : IEfco<VoucherMPE>, IVoucher
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("unitcent")]
        public long UnitCent { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return VoucherMapper.New.Joiner(this); }
        }

        public VoucherMPE Map()
        {
            return VoucherMapper.New.Map<VoucherMPE>(this);
        }
        #endregion
    }

    public class VoucherMapper
        : IMapper<IVoucher>
    {
        public static VoucherMapper New { get; } = new VoucherMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IVoucher e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 3, e1.Currency),
                ('R', 10, e1.UnitCent),
                ('L', 20, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            IVoucher e1) where E : IVoucher, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Currency = e1.Currency,
                UnitCent = e1.UnitCent,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
