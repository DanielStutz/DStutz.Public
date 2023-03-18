using System.Text;

namespace DStutz.System.Joiners
{
    internal class TableRow
    {
        #region Properties
        /***********************************************************/
        private JoinerTable Table { get; }
        private string?[] Cells { get; }
        private ISet<string>? Remarks { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public TableRow(
            JoinerTable table,
            params string?[] cells)
        {
            Table = table;
            Cells = cells;
        }

        public TableRow(
            JoinerTable table,
            int cols)
            : this(table, new string?[cols])
        { }
        #endregion

        #region Methods - Cells
        /***********************************************************/
        internal void Add(
            int col,
            string? item)
        {
            Table.CheckColIndex(col);

            Cells[col] = item;
        }

        internal string? GetCell(
            int col)
        {
            Table.CheckColIndex(col);

            return Cells[col];
        }

        internal int GetCellLenght(
            int col)
        {
            Table.CheckColIndex(col);

            if (Cells[col] == null)
                return 0;

            return Cells[col]!.Length;
        }
        #endregion

        #region Methods - Remarks
        /***********************************************************/
        internal void AddRemark(
            int row,
            string message)
        {
            Table.CheckRowIndex(row);

            if (Remarks == null)
                Remarks = new SortedSet<string>();

            Remarks.Add(message);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Join(", ", Cells));

            if (Remarks != null)
                sb.Append(", " + string.Join(", ", Remarks));

            return sb.ToString();
        }

        internal void AddFormattedCells(
            StringBuilder sb)
        {
            sb.Append(FormatCell(0));

            for (int col = 1; col < Cells.Length; col++)
                sb.Append(Table.GetDelimiter() + FormatCell(col));

            if (Remarks != null)
                sb.Append(Table.GetDelimiter() + string.Join(", ", Remarks));

            sb.AppendLine();
        }

        private string FormatCell(
            int col)
        {
            var cell = Cells[col] ?? "";
            var align = Table.GetAlign(col);
            var width = Table.GetWidth(col);

            if (align == 'L')
                return cell.PadRight(width);

            if (align == 'R')
                return cell.PadLeft(width);

            throw new Exception(
                $"Non existing align {align}");
        }
        #endregion
    }
}
