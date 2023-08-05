using DStutz.Data.Accounting;
using DStutz.Data.GEN.Accounting;
using DStutz.System.Checkers;

using System.Text.Json.Serialization;

// Version 1.1.0
namespace DStutz.Data.Pocos.Transactions
{
    public interface IPayment
        : IDatedTimed, IOrdered
    {
        public long Pk1 { get; set; }
        public string Currency { get; set; }
        public long UnitCent { get; set; }
        public PaymentType Type { get; set; }
        public int Account { get; set; }
        public string? Remark { get; set; }
    }

    public class PaymentMPE
    : IPayment
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
    }
}
