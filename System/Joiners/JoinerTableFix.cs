namespace DStutz.System.Joiners
{
    // The number of cols is fixed
    // The number of rows is fixed
    public class JoinerTableFix
        : JoinerTable
    {
        #region Constructors
        /***********************************************************/
        public JoinerTableFix(
            int rows,
            int cols)
            : base(rows, cols)
        { }
        #endregion

        #region Methods adding first cell (top left)
        /***********************************************************/
        public void AddFirst(
            object? item)
        {
            Add(0, 0, item);
        }

        public void AddFirst(
            string format,
            object item)
        {
            Add(0, 0, format, item);
        }
        #endregion

        #region Methods adding last cell (bottom right)
        /***********************************************************/
        public void AddLast(
            object? item)
        {
            Add(Rows - 1, Cols - 1, item);
        }

        public void AddLast(
            string format,
            object item)
        {
            Add(Rows - 1, Cols - 1, format, item);
        }
        #endregion
    }
}
