namespace DStutz.Sorters.Generic.Trees
{
    /// <summary>
    /// A <c>Assigner</c> is used by a <c>TreeNode</c> to
    /// assign a <c>Member</c> to the correct <c>GroupNode</c>
    /// (a node in the tree).
    /// </summary>
    public abstract class Assigner<G, M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        private SortComparersString Comparers { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected Assigner(
            params SortOrder[] orders)
        {
            Comparers = new SortComparersString(orders);
        }

        protected Assigner(
            IComparer<string> comparer,
            params SortOrder[] orders)
        {
            Comparers = new SortComparersString(comparer, orders);
        }
        #endregion

        #region Methods assigning
        /***********************************************************/
        public abstract string GetKey(M member, int level);
        public abstract string GetName(M member, int level);

        public virtual G GetRootGroup()
        {
            return new G();
        }

        public virtual G GetGroup(M member, int level)
        {
            return new G()
            {
                Level = level,
                Key = GetKey(member, level),
                Name = GetName(member, level),
            };
        }

        public virtual void UpdateGroup(G group, M member)
        {
            // Override to e.g. sum up some data
        }
        #endregion

        #region Methods comparing
        /***********************************************************/
        public int GetLevelCount()
        {
            return Comparers.Count;
        }

        public IComparer<string>? GetComparer(
            int level)
        {
            return Comparers.GetComparer(level);
        }
        #endregion
    }
}
