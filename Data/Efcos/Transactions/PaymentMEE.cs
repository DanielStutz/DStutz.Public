using DStutz.Data.Pocos.Transactions;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Transactions
{
    [Table("payment")]
    public class PaymentMEE
        : IEfco<PaymentMPE>, IPayment
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("unitcent")]
        public long UnitCent { get; set; }

        [Column("type")]
        public PaymentType Type { get; set; }

        [Column("account")]
        public int Account { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return PaymentMapper.New.Joiner(this); }
        }

        public PaymentMPE Map()
        {
            return PaymentMapper.New.Map<PaymentMPE>(this);
        }
        #endregion
    }

    public class PaymentMapper
        : IMapper<IPayment>
    {
        public static PaymentMapper New { get; } = new PaymentMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IPayment e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('R', 2, e1.OrderBy),
                ('L', 10, e1.Date.ToShortDateString()),
                ('L', 3, e1.Currency),
                ('R', 10, e1.UnitCent),
                ('L', 3, e1.Type),
                ('L', 4, e1.Account),
                ('L', 20, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            IPayment e1) where E : IPayment, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                Date = e1.Date,
                Currency = e1.Currency,
                UnitCent = e1.UnitCent,
                Type = e1.Type,
                Account = e1.Account,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
