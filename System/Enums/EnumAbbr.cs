namespace DStutz.System.Enums
{
    public interface IEnumAbbr
    {
        public string Abbr { get; }
    }

    public abstract class EnumAbbr<E>
        : IEnumAbbr, IEnumString<E>, IComparable<IEnumAbbr>
        where E : EnumAbbr<E>
    {
        #region Properties
        /***********************************************************/
        public string Abbr { get; }
        public string Name { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected EnumAbbr()
        { }

        protected EnumAbbr(
            string abbr,
            string name)
        {
            Abbr = abbr;
            Name = name;
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public abstract E Map(string? abbr);

        public string Map(E value)
        {
            if (value.Abbr == null)
                throw new ArgumentNullException("Abbr");

            return value.Abbr;
        }

        public int CompareTo(IEnumAbbr? other)
        {
            if (other == null)
                throw new NullReferenceException();

            return Abbr.CompareTo(other.Abbr);
        }
        #endregion
    }
}
