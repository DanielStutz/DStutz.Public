using DStutz.System.Enums;

namespace DStutz.System.Joiners
{
    // The number of cols is fixed
    // The number of rows can grow
    public class JoinerTableVar
        : JoinerTable
    {
        #region Properties
        /***********************************************************/
        private int ColCursor { get; set; } = 0;
        private int RowCursor { get; set; } = 0;
        #endregion

        #region Constructors
        /***********************************************************/
        public JoinerTableVar(
            int cols)
            : base(cols)
        { }

        public JoinerTableVar(
            params string[] cellsOfFirstRow)
            : base(cellsOfFirstRow.Length)
        {
            AppendRow(cellsOfFirstRow);
        }
        #endregion

        #region Methods adding rows
        /***********************************************************/
        public override void AppendRow(
            params string[] cells)
        {
            base.AppendRow(cells);
            ColCursor = 0;
            RowCursor = Rows - 1;
        }

        public override void AppendEmptyRow()
        {
            AppendRow(new string[Cols]);
        }

        public override void PrependRow(
            params string[] cells)
        {
            base.PrependRow(cells);
            ColCursor = 0;
            RowCursor = 0;
        }

        public override void PrependEmptyRow()
        {
            PrependRow(new string[Cols]);
        }
        #endregion

        #region Methods adding cells
        /***********************************************************/
        public void Add(
            string? item)
        {
            Add(RowCursor, ColCursor++, item);
        }

        public void Add(
            object? item)
        {
            Add(RowCursor, ColCursor++, item);
        }

        public void Add(
            IEnumAbbr? item)
        {
            Add(RowCursor, ColCursor++, item);
        }
        #endregion

        #region Methods adding formatted cells
        /***********************************************************/
        public void Add(
            string format,
            object item)
        {
            Add(RowCursor, ColCursor++, format, item);
        }

        public void Add(
            string format,
            object item0, object item1)
        {
            Add(RowCursor, ColCursor++, format, item0, item1);
        }

        public void Add(
            string format,
            object item0, object item1, object item2)
        {
            Add(RowCursor, ColCursor++, format, item0, item1, item2);
        }

        public void Add(
            string format,
            params object[] items)
        {
            Add(RowCursor, ColCursor++, format, items);
        }
        #endregion

        #region Methods adding remarks
        /***********************************************************/
        public void AddRemark(
            string message)
        {
            AddRemark(RowCursor, message);
        }

        public void AddRemark<M>(
            string message)
        {
            AddRemark<M>(RowCursor, message);
        }

        public void AddRemark<M>()
        {
            AddRemark<M>(RowCursor);
        }
        #endregion
    }
}
