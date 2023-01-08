using DStutz.System.Joiners;

namespace DStutz.Sorters.Generic.Tables
{
    public class Table<G, M>
        where G : IGroupCell<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        public List<string> Rows { get; private set; }
        public List<string> Cols { get; private set; }
        #endregion

        #region Fields
        /***********************************************************/
        private Assigner<G, M> _assigner;
        private G[,] _cells;
        #endregion

        #region Constructors
        /***********************************************************/
        internal Table(Assigner<G, M> assigner)
        {
            _assigner = assigner;
        }
        #endregion

        #region Methods initializing
        /***********************************************************/
        internal void InitColsAndRows(
            IEnumerable<M> members,
            IEnumerable<string>? additionalCols = null,
            IEnumerable<string>? additionalRows = null)
        {
            ISet<string> cols = new SortedSet<string>();
            ISet<string> rows = new SortedSet<string>();

            foreach (var member in members)
            {
                cols.Add(_assigner.GetCol(member));
                rows.Add(_assigner.GetRow(member));
            }

            if (additionalCols != null)
                foreach (var col in additionalCols)
                    cols.Add(col);

            if (additionalRows != null)
                foreach (var row in additionalRows)
                    rows.Add(row);

            InitColsAndRows(cols, rows);
        }

        internal void InitColsAndRows(
            IEnumerable<string> cols,
            IEnumerable<string> rows)
        {
            Rows = new List<string>(rows);
            Cols = new List<string>(cols);

            _cells = new G[Rows.Count, Cols.Count];

            for (int row = 0; row < Rows.Count; row++)
                for (int col = 0; col < Cols.Count; col++)
                    _cells[row, col] = _assigner.GetGroup(Rows[row], Cols[col]);
        }
        #endregion

        #region Methods groups
        /***********************************************************/
        public G GetGroup(int row, int col)
        {
            return _cells[row, col];
        }
        #endregion

        #region Methods members
        /***********************************************************/
        public void Add(M member)
        {
            var row = Rows.IndexOf(_assigner.GetRow(member));
            var col = Cols.IndexOf(_assigner.GetCol(member));

            if (0 <= row && row < Rows.Count &&
                0 <= col && col < Cols.Count)
            {
                var group = _cells[row, col];
                group.Add(member);
                _assigner.UpdateGroup(group, member);
            }
        }

        public void RowOrder(IHandlerCell<G, M> handler)
        {
            for (int row = 0; row < Rows.Count; row++)
                for (int col = 0; col < Cols.Count; col++)
                    Handle(handler, _cells[row, col]);
        }

        public void ColOrder(IHandlerCell<G, M> handler)
        {
            for (int col = 0; col < Cols.Count; col++)
                for (int row = 0; row < Rows.Count; row++)
                    Handle(handler, _cells[row, col]);
        }

        private void Handle(IHandlerCell<G, M> handler, G group)
        {
            foreach (var member in group.Members)
                handler.Handle(group, member);
        }
        #endregion

        #region Methods arrays
        /***********************************************************/
        public string[,] Get2DArray()
        {
            // System.Text.Json can't serialize multidimensional arrays
            var cells = new string[Rows.Count + 1, Cols.Count + 1];

            // Set top left element
            cells[0, 0] = "";

            // Set row names in first col
            for (int row = 0; row < Rows.Count; row++)
                cells[row + 1, 0] = Rows[row];

            // Set col names in first row
            for (int col = 0; col < Cols.Count; col++)
                cells[0, col + 1] = Cols[col];

            // Set data of other cells
            for (int row = 0; row < Rows.Count; row++)
                for (int col = 0; col < Cols.Count; col++)
                    cells[row + 1, col + 1] = GetGroup(row, col).ToString()!;

            return cells;
        }

        public string[][] GetJaggedArray()
        {
            // System.Text.Json can serialize jagged arrays
            var cells = new string[Rows.Count + 1][];

            for (int i = 0; i <= Rows.Count; i++)
                cells[i] = new string[Cols.Count + 1];

            // Set top left element
            cells[0][0] = "";

            // Set row names in first col
            for (int row = 0; row < Rows.Count; row++)
                cells[row + 1][0] = Rows[row];

            // Set col names in first row
            for (int col = 0; col < Cols.Count; col++)
                cells[0][col + 1] = Cols[col];

            // Set data of other cells
            for (int row = 0; row < Rows.Count; row++)
                for (int col = 0; col < Cols.Count; col++)
                    cells[row + 1][col + 1] = GetGroup(row, col).ToString()!;

            return cells;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            JoinTableFix jt = new
                JoinTableFix(Rows.Count + 1, Cols.Count + 1);

            jt.SetAlignsRight();

            // Set row names in first col
            for (int row = 0; row < Rows.Count; row++)
                jt.Add(row + 1, 0, Rows[row]);

            // Set col names in first row
            for (int col = 0; col < Cols.Count; col++)
                jt.Add(0, col + 1, Cols[col]);

            // Set data of other cells
            for (int row = 0; row < Rows.Count; row++)
                for (int col = 0; col < Cols.Count; col++)
                    jt.Add(row + 1, col + 1, _cells[row, col]);

            return jt.ToString();
        }
        #endregion
    }
}
