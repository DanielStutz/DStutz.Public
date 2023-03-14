using DStutz.System.Joiners;

using DStutz.Data.Efcos.Accounting;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IVoucher
    {
        public long Pk1 { get; set; }
        public string Currency { get; set; }
        public long UnitCent { get; set; }
        public string? Remark { get; set; }
    }

    public class VoucherMPE
        : IPoco<IVoucher>, IVoucher
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Currency { get; set; }
        public long UnitCent { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return VoucherMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IVoucher, new()
        {
            return VoucherMapper.New.Map<E>(this);
        }
        #endregion
    }
}
