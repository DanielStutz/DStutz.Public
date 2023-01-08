namespace DStutz.Sorters.Generic
{
    public class SortComparer<T> : IComparer<T>
    {
        #region Properties
        /***********************************************************/
        private IComparer<T> Comparer { get; }
        private SortOrder Order { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public SortComparer(
            SortOrder order)
            : this(Comparer<T>.Default, order)
        { }

        public SortComparer(
            IComparer<T> comparer,
            SortOrder order)
        {
            Comparer = comparer;
            Order = order;
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public int Compare(T? x, T? y)
        {
            if (Order == SortOrder.Ascending)
                return Comparer.Compare(x, y);
            else
                return Comparer.Compare(y, x);
        }
        #endregion
    }

    public class SortComparerString : SortComparer<string>
    {
        #region Constructors
        /***********************************************************/
        public SortComparerString(
            SortOrder order)
            : base(StringComparer.CurrentCulture, order)
        { }

        public SortComparerString(
            IComparer<string> comparer,
            SortOrder order)
            : base(comparer, order)
        { }
        #endregion
    }
}
