using DStutz.System.Enums;

namespace DStutz.Data.Pocos.People
{
    public sealed class FeedbackType
        : EnumAbbr<FeedbackType>
    {
        #region Properties
        /***********************************************************/
        public static readonly FeedbackType P = new FeedbackType("P", "Positive");
        public static readonly FeedbackType N = new FeedbackType("N", "Negative");
        #endregion

        #region Constructors
        /***********************************************************/
        public FeedbackType()
            : base() { }

        private FeedbackType(string abbr, string name)
            : base(abbr, name) { }
        #endregion

        #region Methods converting
        /***********************************************************/
        public override FeedbackType Map(string? abbr)
        {
            if (abbr == null)
                throw new ArgumentNullException("Abbr");

            switch (abbr)
            {
                case "P":
                    return P;
                case "N":
                    return N;
                default:
                    throw EntityNotFoundException(this);
            }
        }
        #endregion
    }
}
