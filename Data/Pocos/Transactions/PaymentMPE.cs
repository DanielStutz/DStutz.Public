using DStutz.System.Checkers;
using DStutz.System.Joiners;

using DStutz.Data.Accounting;
using DStutz.Data.Efcos.Transactions;

using System.Text.Json.Serialization;

// Version 1.1
namespace DStutz.Data.Pocos.Transactions
{
    public interface IPayment
        : IDated, IOrdered
    {
        public long Pk1 { get; set; }
        public string Currency { get; set; }
        public long UnitCent { get; set; }
        public PaymentType Type { get; set; }
        public int Account { get; set; }
        public string? Remark { get; set; }
    }

    public class PaymentMPE
    : IPoco<IPayment>, IPayment
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        [JsonPropertyName("Number")] public int OrderBy { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public long UnitCent { get; set; }
        public PaymentType Type { get; set; }
        public int Account { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code (checking and calculating)
        /***********************************************************/
        public bool HasType(PaymentType type)
        {
            return Type.Equals(type);
        }

        public void Add(Amount amount)
        {
            Checker.Check(
                "Currency",
                amount.Currency, Currency);

            UnitCent += amount.UnitCent;
        }

        public void Sub(Amount amount)
        {
            Checker.Check(
                "Currency",
                amount.Currency, Currency);

            UnitCent -= amount.UnitCent;
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return PaymentMapper.New.Joiner(this);
        }

        public E Map<E>() where E : IPayment, new()
        {
            return PaymentMapper.New.Map<E>(this);
        }
        #endregion
    }
}
