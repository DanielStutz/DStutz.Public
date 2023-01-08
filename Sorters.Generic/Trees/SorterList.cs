namespace DStutz.Sorters.Generic.Trees
{
    /// <summary>
    /// A <c>SorterList</c> is a <c>Sorter</c>
    /// using a <c>List</c> as linear data structure.
    /// </summary>
    public abstract class SorterList<G, M> : Sorter<G, M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        // These linear members are NOT ordered
        public ICollection<M> MembersLinear { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected SorterList(
            Assigner<G, M> assigner)
            : base(assigner)
        {
            MembersLinear = new List<M>();
        }
        #endregion

        #region Methods members
        /***********************************************************/
        public void AddRange(IEnumerable<M> members)
        {
            foreach (var member in members)
                Add(member);
        }

        public virtual void Add(M member)
        {
            MembersLinear.Add(member);
            Root.Add(member);
        }
        #endregion

        #region Methods handling members (linear data structure)
        /***********************************************************/
        public override void Linear(IHandlerNode<G, M> handler)
        {
            foreach (var member in MembersLinear)
                handler.Handle(member);
        }
        #endregion
    }
}
