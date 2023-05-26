using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("booking_entry")]
    public class BookingEntryMEE
        : IEfco<BookingEntryMPE>, IBookingEntry
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("debit")]
        public int Debit { get; set; }

        [Column("credit")]
        public int Credit { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("unitcent")]
        public long UnitCent { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return BookingEntryMapper.New.Joiner(this); }
        }

        public BookingEntryMPE Map()
        {
            return BookingEntryMapper.New.Map<BookingEntryMPE>(this);
        }
        #endregion
    }

    public class BookingEntryMapper
        : IMapper<IBookingEntry>
    {
        public static BookingEntryMapper New { get; } = new BookingEntryMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IBookingEntry e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 10, e1.Date),
                ('L', 4, e1.Debit),
                ('L', 4, e1.Credit),
                ('L', 3, e1.Currency),
                ('R', 10, e1.UnitCent),
                ('L', 100, e1.Text),
                ('L', 40, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            IBookingEntry e1) where E : IBookingEntry, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Date = e1.Date,
                Debit = e1.Debit,
                Credit = e1.Credit,
                Currency = e1.Currency,
                UnitCent = e1.UnitCent,
                Text = e1.Text,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
