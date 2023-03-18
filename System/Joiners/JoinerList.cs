using DStutz.System.Enums;

namespace DStutz.System.Joiners
{
    public class JoinerList
    {
        #region Properties
        /***********************************************************/
        private List<string> Items { get; } = new List<string>();
        private string Delimiter1 { get; }
        private string Delimiter2 { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public JoinerList()
            : this("", "")
        { }

        public JoinerList(
            string delimiter)
            : this(delimiter, "")
        { }

        public JoinerList(
            string delimiter,
            string lastDelimiter)
        {
            Delimiter1 = delimiter;
            Delimiter2 = lastDelimiter;
        }
        #endregion

        #region Methods joining
        /***********************************************************/
        public void Add(
            object? item)
        {
            if (item != null)
                Add(item.ToString());
        }

        public void Add(
            IEnumAbbr? item)
        {
            if (item != null)
                Add(item.Abbr);
        }

        public void Add(
            string? item)
        {
            if (item != null && item.Length > 0)
                Items.Add(item);
        }

        public void AddRange<T>(
            ICollection<T> items)
        {
            if (items != null && items.Count > 0)
                foreach (var item in items)
                    Add(item);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            return string.Join(Delimiter1, Items) + Delimiter2;
        }
        #endregion
    }
}
