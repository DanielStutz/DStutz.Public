namespace DStutz.Data
{
    public class JoinerWidth : Joiner
    {
        #region Properties
        /***********************************************************/
        private List<(object? content, int width)> Cells { get; }
        protected override int Count { get { return Cells.Count; } }
        #endregion

        #region Constructors
        /***********************************************************/
        public JoinerWidth(
            params (object? content, int width)[] cells)
            : base(" | ")
        {
            Cells = new List<(object? content, int width)>(cells);
        }
        #endregion

        #region Methods adding other joinables as cells
        /***********************************************************/
        public override void Add(
            string cell)
        {
            Cells.Add((cell, cell.Length));
        }
        #endregion

        #region Methods joining table row (cells left aligned)
        /***********************************************************/
        public override string Cell(
            int index)
        {
            var (content, width) = Cells[index];

            return Fix(content, width);
        }
        #endregion
    }
}
