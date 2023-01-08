namespace DStutz.Collections.Generic.Collectors
{
    public abstract class MCollection<M>
    {
        #region Properties
        /***********************************************************/
        public abstract int Count { get; }
        public abstract ICollection<M> Members { get; }
        #endregion

        #region Methods members
        /***********************************************************/
        public void Add<S, T>(
            IEnumerable<S?>? items,
            Func<S, T> selector1,
            Func<T, M> selector2)
        {
            if (items != null)
                foreach (var item in items)
                    if (item != null)
                        Add(selector2(selector1(item)));
        }

        public void Add<T>(
            IEnumerable<T?>? items,
            Func<T, M> selector)
        {
            if (items != null)
                foreach (var item in items)
                    if (item != null)
                        Add(selector(item));
        }

        public abstract void Add(M member);
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public abstract void Write();
        #endregion
    }
}
