namespace DStutz.Sorters.Generic.Trees
{
    public abstract class GroupNodeDictionary<K, M>
        : GroupDictionary<K, M>, IGroupNode<M>
        where K : notnull
        where M : notnull
    {
        #region Properties
        /***********************************************************/
        public int Level { get; set; }
        public string Key { get; set; } = "";
        public string Name { get; set; } = "Root";
        #endregion

        #region Constructors
        /***********************************************************/
        public GroupNodeDictionary()
        { }

        public GroupNodeDictionary(
            SortOrder order)
            : base(order)
        { }

        public GroupNodeDictionary(
            IComparer<K>? comparer)
            : base(comparer)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public override string ToStringGroup()
        {
            return GroupNode.ToString(Level, Key, Name, MembersCount);
        }
        #endregion
    }
}
