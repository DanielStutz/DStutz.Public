using DStutz.System.Extensions;

using System.Text;
using System.Text.RegularExpressions;

namespace DStutz.System.Joiners
{
    public class Joiner : IJoiner
    {
        #region Properties
        /***********************************************************/
        private string Separator { get; }
        private List<(char align, int width, object? content)> Cells { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public Joiner(
            params (int width, object? content)[] cells)
            : this(" | ", cells)
        { }

        public Joiner(
            string separator,
            params (int width, object? content)[] cells)
        {
            Separator = separator;
            Cells = new List<(char align, int width, object? content)>();

            foreach (var cell in cells)
                Cells.Add(('L', cell.width, cell.content));
        }

        public Joiner(
            params (char align, int width, object? content)[] cells)
            : this(" | ", cells)
        { }

        public Joiner(
            string separator,
            params (char align, int width, object? content)[] cells)
        {
            Separator = separator;
            Cells = new List<(char align, int width, object? content)>(cells);
        }
        #endregion

        #region Methods adding other joinables
        /***********************************************************/
        public IJoiner Add(
            params IJoinable?[] joinables)
        {
            foreach (var joinable in joinables)
                if (joinable != null)
                    Add('L', joinable.Joiner().Row());

            return this;
        }

        private void Add(
            char align,
            string cell)
        {
            Cells.Add((align, cell.Length, cell));
        }
        #endregion

        #region Methods adding other cells
        /***********************************************************/
        public IJoiner Add(
            params (char align, int width, object? content)[] cells)
        {
            Cells.AddRange(cells);

            return this;
        }
        #endregion

        #region Methods adding other objects as cells
        /***********************************************************/
        public IJoiner Add(
            char align,
            int width,
            params object?[] cells)
        {
            foreach (var cell in cells)
                if (cell != null)
                    Add(align, width, cell);

            return this;
        }

        private void Add(
            char align,
            int width,
            object cell)
        {
            Cells.Add((align, width, cell));
        }
        #endregion

        #region Methods joining table row
        /***********************************************************/
        public string Join(
            string separator)
        {
            var sb = new StringBuilder();

            sb.Append(Cell(0));

            for (int i = 1; i < Cells.Count; i++)
                sb.Append(separator + Cell(i));

            return sb.ToString();
        }

        private string Cell(
            int index)
        {
            var (align, width, content) = Cells[index];

            if (content == null)
                return string.Format("{0," + width + "}", "");

            return content.ToString().Fix(width, align);
        }
        #endregion

        #region Methods joining
        /***********************************************************/
        public string Col()
        {
            return Join("\n");
        }

        public string Row()
        {
            return Join(Separator);
        }

        public string RowShort()
        {
            var row = Join(", ");

            // Some cells might contain other joiners
            row = row.Replace(" | ", ", ");

            // Remove multiple whitespaces by one
            row = Regex.Replace(row, @"\s+", " ");

            // Remove whitespaces before comma
            row = row.Replace(" ,", ",");

            // Indicate empty cells
            row = row.Replace(",,", ", ?,");

            return row;
        }
        #endregion

        #region Methods writing
        /***********************************************************/
        public void WriteCol()
        {
            Console.WriteLine(Col());
        }

        public void WriteRow()
        {
            Console.WriteLine(Row());
        }

        public void WriteRowShort()
        {
            Console.WriteLine(RowShort());
        }
        #endregion
    }
}
