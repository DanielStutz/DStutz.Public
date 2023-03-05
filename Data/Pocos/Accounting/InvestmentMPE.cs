using DStutz.System.Joiners;

using DStutz.Data.Efcos.Accounting;
using DStutz.Data.Pocos.Contacts;
using DStutz.Data.Pocos.Logistics;

// Version 1.1
namespace DStutz.Data.Pocos.Accounting
{
    public interface IInvestment
    {
        public int Pk1 { get; set; } // Account number
        public int OrderBy { get; set; }
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
        public AddressMPE? CreditorAddress { get; set; }

        public long? CreditorContactPk1 { get; set; }
        public ContactMPE? CreditorContact { get; set; }

        public long? DebitorAddressPk1 { get; set; }
        public AddressMPE? DebitorAddress { get; set; }

        public long? DebitorContactPk1 { get; set; }
        public ContactMPE? DebitorContact { get; set; }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return InvestmentMapper.New.Joiner(this);
        }

        public E Map<E>() where E : IInvestment, new()
        {
            return InvestmentMapper.New.Map<E>(this);
        }
        #endregion
    }
}
