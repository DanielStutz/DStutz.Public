using DStutz.System.Enums;

namespace DStutz.Data.Pocos.Transactions
{
    public sealed class TransactionStatus
        : EnumAbbr<TransactionStatus>
    {
        #region Properties
        /***********************************************************/
        public static readonly TransactionStatus COM = new TransactionStatus("COM", "Transaction completed");
        public static readonly TransactionStatus PEN = new TransactionStatus("PEN", "Transaction pending");
        public static readonly TransactionStatus RES = new TransactionStatus("RES", "Transaction rescinded");
        public static readonly TransactionStatus ZOM = new TransactionStatus("ZOM", "Zombie (Testing)");
        #endregion

        #region Constructors
        /***********************************************************/
        public TransactionStatus()
            : base() { }

        private TransactionStatus(string abbr, string name)
            : base(abbr, name) { }
        #endregion

        #region Methods converting
        /***********************************************************/
        public override TransactionStatus Map(string? abbr)
        {
            if (abbr == null)
                throw new ArgumentNullException("Abbr");

            switch (abbr)
            {
                case "COM":
                    return COM;
                case "PEN":
                    return PEN;
                case "RES":
                    return RES;
                case "ZOM":
                    return ZOM;
                default:
                    throw NotFoundException(this);
            }
        }
        #endregion
    }
}
