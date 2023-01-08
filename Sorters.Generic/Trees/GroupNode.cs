using DStutz.System.Extensions;

namespace DStutz.Sorters.Generic.Trees
{
    public interface IGroupNode<M>
        : IGroup<M>
        where M : notnull
    {
        public int Level { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }

    public abstract class GroupNode
    {
        #region Miscellaneous
        /***********************************************************/
        public static string ToString(
            int level,
            string key,
            string? name,
            int count)
        {
            return string.Format(
                "Le = {0,2} | Ke = {1,-10} | Na = {2,-20} | Me = {3,3}",
                level, key.Max(10), name.Max(20), count);
        }
        #endregion
    }
}
