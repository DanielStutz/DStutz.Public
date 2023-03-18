using DStutz.System.Enums;

using System.Text;

namespace DStutz.System.Joiners
{
    public abstract class JoinerTable
    {
        #region Properties
        /***********************************************************/
        public int Cols { get; }
        public int Rows { get { return AllRows.Count; } }
        public string? Header { get; set; } // 
        private char[] Aligns { get; set; }
        private string Delimiter { get; set; } = " | ";
        private int[] Widths { get; set; }
        private List<TableRow> AllRows { get; } = new List<TableRow>();
        #endregion

        #region Constructors
        /***********************************************************/
        protected JoinerTable(
            int cols)
        {
            // Used for a table with a variable number of rows
            Cols = cols;
            SetAlignsLeft();
        }

        protected JoinerTable(
            int rows,
            int cols)
            : this(cols)
        {
            // Used for a table with a fixed number of rows
            for (int row = 0; row < rows; row++)
                AllRows.Add(new TableRow(this, cols));
        }
        #endregion

        #region Methods handling align
        /***********************************************************/
        public int GetAlign(
            int col)
        {
            CheckColIndex(col);
            return Aligns[col];
        }

        public void SetAligns(
            char[] aligns)
        {
            CheckColNumber(aligns.Length);

            for (int i = 0; i < aligns.Length; i++)
                SetAlign(i, aligns[i]);
        }

        public void SetAligns(
            string aligns)
        {
            CheckColNumber(aligns.Length);

            for (int i = 0; i < aligns.Length; i++)
                SetAlign(i, aligns[i]);
        }

        public void SetAlign(
            int col,
            char align)
        {
            CheckColIndex(col);

            if (align != 'L' &&
                align != 'R')
                throw new Exception(
                    $"Non existing align {align}");

            Aligns[col] = align;
        }

        public void SetAlignLeft(
            int col)
        {
            CheckColIndex(col);
            Aligns[col] = 'L';
        }

        public void SetAlignsLeft()
        {
            Aligns = Enumerable.Repeat('L', Cols).ToArray();
        }

        public void SetAlignRight(
            int col)
        {
            CheckColIndex(col);
            Aligns[col] = 'R';
        }

        public void SetAlignsRight()
        {
            Aligns = Enumerable.Repeat('R', Cols).ToArray();
        }
        #endregion

        #region Methods handling delimiter and headers
        /***********************************************************/
        public string GetDelimiter()
        {
            return Delimiter;
        }

        public void SetDelimiter(
            string delimiter)
        {
            Delimiter = delimiter;
        }

        public void AddColHeader(
            int col,
            string item,
            char align = 'R')
        {
            AllRows[0].Add(col, item);
            Aligns[col] = align;
        }

        public void AddTableHeaders(
            string format,
            params string[] headers)
        {
            Header = string.Format(format, headers);
        }
        #endregion

        #region Methods handling width
        /***********************************************************/
        public int GetWidth(
            int col)
        {
            CheckColIndex(col);
            return Widths[col];
        }

        public void SetWidths(
            int[] widths)
        {
            CheckColNumber(widths.Length);
            Widths = widths;
        }

        private void CalculateWidths()
        {
            if (Widths == null)
                Widths = new int[Cols];

            for (int col = 0; col < Cols; col++)
                for (int row = 0; row < Rows; row++)
                    Widths[col] =
                        global::System.Math.Max(
                            Widths[col],
                            AllRows[row].GetCellLenght(col));
        }
        #endregion

        #region Methods adding rows
        /***********************************************************/
        public virtual void AppendRow(
            params string[] cells)
        {
            CheckColNumber(cells.Length);
            AllRows.Add(new TableRow(this, cells));
        }

        public virtual void AppendEmptyRow()
        {
            AppendRow(new string[Cols]);
        }

        public virtual void PrependRow(
            params string[] cells)
        {
            CheckColNumber(cells.Length);
            AllRows.Insert(0, new TableRow(this, cells));
        }

        public virtual void PrependEmptyRow()
        {
            PrependRow(new string[Cols]);
        }
        #endregion

        #region Methods adding cells
        /***********************************************************/
        public void Add(
            int row,
            int col,
            string? item)
        {
            CheckRowIndex(row);

            AllRows[row].Add(col, item);
        }

        public void Add(
            int row,
            int col,
            object? item)
        {
            if (item != null)
                Add(row, col, item.ToString());
        }

        public void Add<T>(
            int row,
            int col,
            ICollection<T>? item)
        {
            if (item != null && item.Count > 0)
                Add(row, col, item.Count);
        }

        public void Add<T>(
            int row,
            int col,
            bool item)
        {
            if (item)
                Add(row, col, "T");
        }

        public void Add(
            int row,
            int col,
            IEnumAbbr? item)
        {
            if (item != null)
                Add(row, col, item.Abbr);
        }

        public string? GetCell(
            int row,
            int col)
        {
            CheckRowIndex(row);

            return AllRows[row].GetCell(col);
        }

        public int GetCellLenght(
            int row,
            int col)
        {
            CheckRowIndex(row);

            return AllRows[row].GetCellLenght(col);
        }
        #endregion

        #region Methods adding formatted cells
        /***********************************************************/
        public void Add(
            int row,
            int col,
            string format,
            object item)
        {
            Add(row, col, string.Format(format, item));
        }

        public void Add(
            int row,
            int col,
            string format,
            object item0,
            object item1)
        {
            Add(row, col, string.Format(format, item0, item1));
        }

        public void Add(
            int row,
            int col,
            string format,
            object item0,
            object item1,
            object item2)
        {
            Add(row, col, string.Format(format, item0, item1, item2));
        }

        public void Add(
            int row,
            int col,
            string format,
            params object[] items)
        {
            Add(row, col, string.Format(format, items));
        }
        #endregion

        #region Methods adding remarks
        /***********************************************************/
        public void AddRemark(
            int row,
            string message)
        {
            AllRows[row].AddRemark(row, message);
        }

        public void AddRemark<M>(
            int row,
            string message)
        {
            AddRemark(row, $"{typeof(M).Name} {message}");
        }

        public void AddRemark<M>(
            int row)
        {
            AddRemark(row, $"{typeof(M).Name}");
        }
        #endregion

        #region Methods checking
        /***********************************************************/
        public void CheckColNumber(
            int cols)
        {
            if (cols != Cols)
                throw new Exception(
                    $"Number of cols {cols} != {Cols}");
        }

        public void CheckRowNumber(
            int rows)
        {
            if (rows != Rows)
                throw new Exception(
                    $"Number of rows {rows} != {Rows}");
        }

        public void CheckColIndex(
            int col)
        {
            if (col < 0 || col > Cols - 1)
                throw new Exception(
                    $"Non existing col {col}");
        }

        public void CheckRowIndex(
            int row)
        {
            if (row < 0 || row > Rows - 1)
                throw new Exception(
                    $"Non existing row {row}");
        }

        public void CheckIndices(
            int row,
            int col)
        {
            CheckRowIndex(row);
            CheckColIndex(col);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            if (Widths == null)
                CalculateWidths();

            var sb = new StringBuilder();

            if (Header != null)
                sb.AppendLine(Header);

            foreach (var row in AllRows)
                row.AddFormattedCells(sb);

            return sb.ToString();
        }
        #endregion
    }
}
