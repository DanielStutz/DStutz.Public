using DStutz.Data.BLO.Logistics;
using DStutz.Data.Efcos.Accounting;

// Version 1.1.0
namespace DStutz.Data.Pocos.Accounting
{
    public interface IInvestment
        : IOrdered
    {
        public int Pk1 { get; set; } // Account number
        public DateTime Date1 { get; set; } // First payment
        public DateTime? Date2 { get; set; } // Last payment
        public string CreditorName { get; set; }
        public string? Remark { get; set; }
        public long? CreditorAddressPk1 { get; set; }
        public long? CreditorContactPk1 { get; set; }
        public long? DebitorAddressPk1 { get; set; }
        public long? DebitorContactPk1 { get; set; }
    }

    public class InvestmentMPE
        : IPoco<IInvestment>, IInvestment
    {
        #region Properties
        /***********************************************************/
        public int Pk1 { get; set; } // Account number
        public int OrderBy { get; set; }
        public DateTime Date1 { get; set; } // First payment
        public DateTime? Date2 { get; set; } // Last payment
        public string CreditorName { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code (keys)
        /***********************************************************/
        public int Number { get { return Pk1; } }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long? CreditorAddressPk1 { get; set; }
        public AddressBLO? CreditorAddress { get; set; }

        public long? CreditorContactPk1 { get; set; }
        public ContactBLO? CreditorContact { get; set; }

        public long? DebitorAddressPk1 { get; set; }
        public AddressBLO? DebitorAddress { get; set; }

        public long? DebitorContactPk1 { get; set; }
        public ContactBLO? DebitorContact { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return InvestmentMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IInvestment, new()
        {
            return InvestmentMapper.New.Map<E>(this);
        }
        #endregion
    }
}
