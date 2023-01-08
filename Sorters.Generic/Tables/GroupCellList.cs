namespace DStutz.Sorters.Generic.Tables
{
    public abstract class GroupCellList<M>
        : GroupList<M>, IGroupCell<M>
        where M : notnull
    {
        #region Properties
        /***********************************************************/
        public string Row { get; set; }
        public string Col { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public GroupCellList()
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
