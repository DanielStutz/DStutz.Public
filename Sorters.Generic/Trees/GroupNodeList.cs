namespace DStutz.Sorters.Generic.Trees
{
    public abstract class GroupNodeList<M>
        : GroupList<M>, IGroupNode<M>
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
        public GroupNodeList()
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
