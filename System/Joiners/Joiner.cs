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

        #region Constructors deprecated
        /***********************************************************/
        [Obsolete("Ctor is deprecated.")]
        public Joiner(
            params (object? content, int width)[] cells)
        {
            Separator = " | ";
            Cells = new List<(char align, int width, object? content)>();

            foreach (var cell in cells)
                Cells.Add(('L', cell.width, cell.content));
        }
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

        #region Methods adding other joinables as cells
        /***********************************************************/
        public IJoiner Add(
            params IJoinable?[] joinables)
        {
            foreach (var joinable in joinables)
                if (joinable != null)
                    Add(joinable.Joiner().Row());

            return this;
        }

        private void Add(
            string cell)
        {
            Cells.Add(('L', cell.Length, cell));
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

        public string RowNoWhitespaces()
        {
            return Regex.Replace(Join(", "), @"\s+", " ").Replace(" ,", ",");
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
        #endregion
    }
}
