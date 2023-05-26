using DStutz.Data.Printers;

namespace DStutz.Data
{
    public abstract class TreeNode<E>
        where E : ITreeNode<E>, IJoinableOld
    {
        public static int CountLevels(
            E node)
        {
            int level = node.Level;

            if (node.Children != null)
                foreach (var child in node.Children)
                    level = global::System.Math.Max(level, TreeNode<E>.CountLevels(child));

            return level;
        }

        public static int CountNodes(
            E node)
        {
            int count = 1;

            if (node.Children != null)
                foreach (var child in node.Children)
                    count += CountNodes(child);

            return count;
        }

        public static void Print(
            E node)
        {
            PrinterTree.Print(node, CountLevels(node), 0);
        }
    }
}
