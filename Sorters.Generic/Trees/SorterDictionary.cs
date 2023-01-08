namespace DStutz.Sorters.Generic.Trees
{
    /// <summary>
    /// A <c>SorterDictionary</c> is a <c>Sorter</c>
    /// using a <c>SortedDictionary</c> as linear data structure.
    /// </summary>
    public abstract class SorterDictionary<G, K, M> : Sorter<G, M>
        where G : IGroupNode<M>, new()
        where K : IComparable<K>
        where M : class
    {
        #region Properties
        /***********************************************************/
        // These linear members are ordered by K
        public IDictionary<K, M> MembersLinear { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected SorterDictionary(
            Assigner<G, M> assigner)
            : base(assigner)
        {
            MembersLinear = new SortedDictionary<K, M>();
        }
        #endregion

        #region Methods members
        /***********************************************************/
        public void AddRange(IDictionary<K, M> members)
        {
            foreach (var member in members)
                Add(member.Key, member.Value);
        }

        public virtual void Add(K key, M member)
        {
            MembersLinear.Add(key, member);
            Root.Add(member);
        }
        #endregion

        #region Methods handling members (linear data structure)
        /***********************************************************/
        public override void Linear(IHandlerNode<G, M> handler)
        {
            foreach (var member in MembersLinear.Values)
                handler.Handle(member);
        }
        #endregion
    }
}
