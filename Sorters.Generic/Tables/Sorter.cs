using System.Text;

namespace DStutz.Sorters.Generic.Tables
{
    /// <summary>
    /// A <c>Sorter</c> holds a <c>Table</c> and also a
    /// linear data structure which holds all its <c>Member</c>.
    /// </summary>
    public abstract class Sorter<G, M> : Sorter<M>
        where G : IGroupCell<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        // TODO Add an interface ITable to Table and make Table private?!
        public Table<G, M> Table { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected Sorter(
            Assigner<G, M> assigner)
        {
            Table = new Table<G, M>(assigner);
        }
        #endregion

        #region Methods initializing
        /***********************************************************/
        public void InitTable(
            IEnumerable<M> members,
            IEnumerable<string>? additionalCols = null,
            IEnumerable<string>? additionalRows = null)
        {
            Table.InitColsAndRows(members, additionalCols, additionalRows);
        }

        public void InitTable(
            IEnumerable<string> cols,
            IEnumerable<string> rows)
        {
            Table.InitColsAndRows(cols, rows);
        }
        #endregion

        #region Methods table
        /***********************************************************/
        public int CountCols()
        {
            return Table.Cols.Count;
        }

        public int CountRows()
        {
            return Table.Rows.Count;
        }

        public G GetGroup(int row, int col)
        {
            return Table.GetGroup(row, col);
        }

        public bool IsLastCol(int col)
        {
            return col == Table.Cols.Count - 1;
        }

        public bool IsLastCol(string col)
        {
            return col.Equals(Table.Cols[^1]);
        }

        public bool IsLastRow(int row)
        {
            return row == Table.Rows.Count - 1;
        }

        public bool IsLastRow(string row)
        {
            return row.Equals(Table.Rows[^1]);
        }
        #endregion

        #region Methods handling members of Table
        /***********************************************************/
        public void PrintRowOrder()
        {
            RowOrder(new HandlerCellConsole<G, M>());
        }

        public void PrintColOrder()
        {
            ColOrder(new HandlerCellConsole<G, M>());
        }

        public void RowOrder(IHandlerCell<G, M> handler)
        {
            Table.RowOrder(handler);
        }

        public void ColOrder(IHandlerCell<G, M> handler)
        {
            Table.ColOrder(handler);
        }
        #endregion

        #region Methods handling members (linear data structure)
        /***********************************************************/
        public void PrintLinear()
        {
            Linear(new HandlerCellConsole<G, M>());
        }

        public abstract void Linear(IHandlerCell<G, M> handler);
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public void WriteTable()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Table.ToString());

            foreach (var error in Errors)
                sb.AppendLine(error);

            Console.WriteLine(sb.ToString());
        }
        #endregion
    }
}
