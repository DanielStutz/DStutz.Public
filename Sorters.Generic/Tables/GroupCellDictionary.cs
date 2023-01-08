namespace DStutz.Sorters.Generic.Tables
{
    public abstract class GroupCellDictionary<K, M>
        : GroupDictionary<K, M>, IGroupCell<M>
        where K : notnull
        where M : notnull
    {
        #region Properties
        /***********************************************************/
        public string Row { get; set; }
        public string Col { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public GroupCellDictionary()
            : this(null)
        { }

        public GroupCellDictionary(
            SortOrder order)
            : this(new SortComparer<K>(order))
        { }

        public GroupCellDictionary(
            IComparer<K>? comparer)
            : base(comparer)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public override string ToStringGroup()
        {
            return GroupCell.ToString(Row, Col, MembersCount);
        }
        #endregion
    }
}
