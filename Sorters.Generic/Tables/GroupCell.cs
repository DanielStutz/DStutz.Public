namespace DStutz.Sorters.Generic.Tables
{
    public interface IGroupCell<M>
        : IGroup<M>
        where M : notnull
    {
        public string Row { get; set; }
        public string Col { get; set; }
    }

    public abstract class GroupCell
    {
        #region Miscellaneous
        /***********************************************************/
        public static string ToString(
            string row,
            string col,
            int count)
        {
            return string.Format(
                "Ro = {0,2} | Co = {1,2} | Me = {2,3}",
                row, col, count);
        }
        #endregion
    }
}
