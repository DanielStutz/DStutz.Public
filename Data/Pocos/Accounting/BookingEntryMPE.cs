using DStutz.Data.Efcos.Accounting;
using DStutz.Data.Accounting;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IBookingEntry
    {
        public long Pk1 { get; set; }
        public DateTime Date { get; set; }
        public int Debit { get; set; }
        public int Credit { get; set; }
        public string Currency { get; set; }
        public long UnitCent { get; set; }
        public string Text { get; set; }
        public string? Remark { get; set; }
    }

    public class BookingEntryMPE
        : IPoco<IBookingEntry>, IBookingEntry, IAccountable
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public DateTime Date { get; set; }
        public int Debit { get; set; }
        public int Credit { get; set; }
        public string Currency { get; set; }
        public long UnitCent { get; set; }
        public string Text { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string Marker { get; } = Accountable.BE;

        public string Identifier
        {
            get
            {
                return Date.ToString("yyyy-MM-dd_HHmm");
            }
        }

        public Amount Amount
        {
            get
            {
                return new Amount(Currency, UnitCent);
            }

            set
            {
                Currency = value.Currency;
                UnitCent = value.UnitCent;
            }
        }

        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return BookingEntryMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IBookingEntry, new()
        {
            return BookingEntryMapper.New.Map<E>(this);
        }
        #endregion
    }
}
