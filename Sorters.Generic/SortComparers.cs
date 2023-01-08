namespace DStutz.Sorters.Generic
{
    public class SortComparers<T>
    {
        #region Properties
        /***********************************************************/
        public IComparer<T>[] Comparers { get; }
        public int Count { get { return Comparers.Length; } }
        #endregion

        #region Constructors
        /***********************************************************/
        public SortComparers(
            params SortOrder[] orders)
            : this(Comparer<T>.Default, orders)
        { }

        public SortComparers(
            IComparer<T> comparer,
            params SortOrder[] orders)
        {
            if (orders == null)
                throw new ArgumentNullException("orders");

            Comparers = new SortComparer<T>[orders.Length];

            for (int i = 0; i < orders.Length; i++)
                Comparers[i] = new SortComparer<T>(comparer, orders[i]);
        }
        #endregion

        #region Methods
        /***********************************************************/
        public IComparer<T>? GetComparer(
            int index)
        {
            if (index < Comparers.Length)
                return Comparers[index];

            return null;
        }
        #endregion
    }

    public class SortComparersString : SortComparers<string>
    {
        #region Constructors
        /***********************************************************/
        public SortComparersString(
            params SortOrder[] orders)
            : base(StringComparer.CurrentCulture, orders)
        { }

        public SortComparersString(
            IComparer<string> comparer,
            params SortOrder[] orders)
            : base(comparer, orders)
        { }
        #endregion
    }
}
