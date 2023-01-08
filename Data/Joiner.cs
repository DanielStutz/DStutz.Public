using DStutz.System.Extensions;

using System.Text;
using System.Text.RegularExpressions;

namespace DStutz.Data
{
    public abstract class Joiner : IJoiner
    {
        #region Properties
        /***********************************************************/
        private string Separator { get; }
        protected abstract int Count { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected Joiner(
            string separator)
        {
            Separator = separator;
        }
        #endregion

        #region Methods adding other joinables as cells
        /***********************************************************/
        public IJoiner Add(
            params IJoinable?[] joinables)
        {
            foreach (var joinable in joinables)
                if (joinable != null)
                    Add(joinable.Join().Row());

            return this;
        }

        public abstract void Add(string cell);
        #endregion

        #region Methods joining table row (cells left aligned)
        /***********************************************************/
        public string Join(
            string separator)
        {
            var sb = new StringBuilder();

            sb.Append(Cell(0));

            for (int i = 1; i < Count; i++)
                sb.Append(separator + Cell(i));

            return sb.ToString();
        }

        public abstract string Cell(int index);
        #endregion

        #region Methods writing and joining
        /***********************************************************/
        public void WriteCol()
        {
            Console.WriteLine(Col());
        }

        public void WriteRow()
        {
            Console.WriteLine(Row());
        }

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

        #region Miscellaneous
        /***********************************************************/
        protected string Fix(
            object? content,
            int length,
            bool leftAlign = true)
        {
            if (content == null)
                return string.Format("{0," + length + "}", "");

            return content.ToString().Fix(length, leftAlign);
        }
        #endregion
    }
}
