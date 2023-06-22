using DStutz.System.Enums;

namespace DStutz.Data.Pocos.People
{
    // Based on a person's identification on the inside
    public sealed class Gender
        : EnumAbbr<Gender>
    {
        #region Properties
        /***********************************************************/
        public static readonly Gender F = new Gender("F", "Female");
        public static readonly Gender M = new Gender("M", "Male");
        public static readonly Gender O = new Gender("O", "Other");
        #endregion

        #region Constructors
        /***********************************************************/
        public Gender()
            : base() { }

        private Gender(string abbr, string name)
            : base(abbr, name) { }
        #endregion

        #region Methods converting
        /***********************************************************/
        public override Gender Map(string? abbr)
        {
            if (abbr == null)
                throw new ArgumentNullException("Abbr");

            switch (abbr)
            {
                case "F":
                    return F;
                case "M":
                    return M;
                case "O":
                    return O;
                default:
                    throw EntityNotFoundException(this);
            }
        }
        #endregion
    }
}
