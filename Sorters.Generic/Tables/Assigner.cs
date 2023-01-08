namespace DStutz.Sorters.Generic.Tables
{
    /// <summary>
    /// A <c>Assigner</c> is used by a <c>Table</c> to
    /// assign a <c>Member</c> to the correct <c>GroupCell</c>
    /// (column and row).
    /// </summary>
    public abstract class Assigner<G, M>
        where G : IGroupCell<M>, new()
        where M : class
    {
        #region Constructors
        /***********************************************************/
        protected Assigner()
        { }
        #endregion

        #region Methods assigning
        /***********************************************************/
        public abstract string GetCol(M member);
        public abstract string GetRow(M member);

        public virtual G GetGroup(string row, string col)
        {
            return new G()
            {
                Row = row,
                Col = col,
            };
        }

        public virtual void UpdateGroup(G group, M member)
        {
            // Override to e.g. sum up some data
        }
        #endregion
    }
}
