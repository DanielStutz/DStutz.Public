using DStutz.System.Enums;

namespace DStutz.Data.Pocos.Transactions
{
    public sealed class PaymentType
        : EnumAbbr<PaymentType>
    {
        #region Properties
        /***********************************************************/
        public static readonly PaymentType APP = new PaymentType("APP", "Apple Pay");
        public static readonly PaymentType BAC = new PaymentType("BAC", "Bank account");
        public static readonly PaymentType CAS = new PaymentType("CAS", "Cash");
        public static readonly PaymentType GOO = new PaymentType("GOO", "Google Pay");
        public static readonly PaymentType PAY = new PaymentType("PAY", "Paypal");
        public static readonly PaymentType SAM = new PaymentType("SAM", "Samsung Pay");
        public static readonly PaymentType TWI = new PaymentType("TWI", "Twint");

        // The account number is NOT part of the payment type but the
        // payment itself, see interface IPayment in file PaymentMPE.cs
        public static readonly PaymentType CGA = new PaymentType("CGA", "Customer give-away"); // 6642
        public static readonly PaymentType COM = new PaymentType("COM", "Compensation");       // ?
        public static readonly PaymentType EXP = new PaymentType("EXP", "Expenses");           // 6622, 6640
        public static readonly PaymentType VOU = new PaymentType("VOU", "Voucher");            // 6640
        public static readonly PaymentType ZOM = new PaymentType("ZOM", "Zombie (PC-P)");      // 1000

        // Creditors (Gläubiger)
        // 2010 (loan from Stutz Toys)
        // see TaxData.AddPurchaseOrders()
        public static readonly PaymentType CRE = new PaymentType("CRE", "Creditors");

        // Debitors (Schuldner)
        // 1110 (loan to Protechnic.ch GmbH, channel LOA)
        // 1111 (loan to Digitec Galaxus AG, channel GAL)
        // see TaxData.AddSaleOrders()
        public static readonly PaymentType DEB = new PaymentType("DEB", "Debitors");
        #endregion

        #region Constructors
        /***********************************************************/
        public PaymentType()
            : base() { }

        private PaymentType(string abbr, string name)
            : base(abbr, name) { }
        #endregion

        #region Methods converting
        /***********************************************************/
        public override PaymentType Map(string? abbr)
        {
            if (abbr == null)
                throw new ArgumentNullException("Abbr");

            switch (abbr)
            {
                case "APP":
                    return APP;
                case "BAC":
                    return BAC;
                case "CAS":
                    return CAS;
                case "CGA":
                    return CGA;
                case "COM":
                    return COM;
                case "CRE":
                    return CRE;
                case "DEB":
                    return DEB;
                case "EXP":
                    return EXP;
                case "GOO":
                    return GOO;
                case "PAY":
                    return PAY;
                case "SAM":
                    return SAM;
                case "TWI":
                    return TWI;
                case "VOU":
                    return VOU;
                case "ZOM":
                    return ZOM;
                default:
                    throw NotFoundException(this);
            }
        }
        #endregion
    }
}
