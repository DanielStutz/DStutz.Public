namespace DStutz.Data
{
    public class JoinerWidthAligned : Joiner
    {
        #region Properties
        /***********************************************************/
        private List<(object? content, int width, bool leftAlign)> Cells { get; }
        protected override int Count { get { return Cells.Count; } }
        #endregion

        #region Constructors
        /***********************************************************/
        public JoinerWidthAligned(
            params (object? content, int width, bool leftAlign)[] cells)
            : base(" | ")
        {
            Cells = new List<(object? content, int width, bool leftAlign)>(cells);
        }
        #endregion

        #region Methods adding other joinables as cells
        /***********************************************************/
        public override void Add(
            string cell)
        {
            Cells.Add((cell, cell.Length, true));
        }
        #endregion

        #region Methods joining table row (cells individually aligned)
        /***********************************************************/
        public override string Cell(
            int index)
        {
            var (content, width, leftAlign) = Cells[index];

            return Fix(content, width, leftAlign);
        }
    }
    #endregion
}
