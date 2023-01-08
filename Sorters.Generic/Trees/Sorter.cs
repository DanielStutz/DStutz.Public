namespace DStutz.Sorters.Generic.Trees
{
    /// <summary>
    /// A <c>Sorter</c> holds a <c>TreeNode</c> as the entry point
    /// (root) to a tree and also a linear data structure which holds
    /// all its <c>Member</c>.
    /// </summary>
    public abstract class Sorter<G, M> : Sorter<M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        // TODO Add an interface ITreeNode to TreeNode and make Root private?!
        public TreeNode<G, M> Root { get; }

        // These members are ordered by the assigner
        public IReadOnlyList<M> Members { get { return GetMembers(); } }
        public IReadOnlyList<M> MembersReverse { get { return GetMembersReverse(); } }
        #endregion

        #region Constructors
        /***********************************************************/
        protected Sorter(
            Assigner<G, M> assigner)
        {
            Root = new TreeNode<G, M>(assigner);
        }
        #endregion

        #region Methods tree
        /***********************************************************/
        //public G GetRootGroup()
        //{
        //    return Root.Group;
        //}

        //public ICollection<TreeNode<G, M>> GetRootNodes()
        //{
        //    return Root.Nodes;
        //}

        public int CountLevels()
        {
            return Root.CountLevels();
        }

        public int CountNodes()
        {
            return Root.CountNodes();
        }

        public int CountMembers()
        {
            return Root.CountMembers();
        }
        #endregion

        #region Methods handling members of Root
        /***********************************************************/
        private IReadOnlyList<M> GetMembers()
        {
            return Preorder(new HandlerNodeCollector<G, M>()).Members;
        }

        private IReadOnlyList<M> GetMembersReverse()
        {
            return Postorder(new HandlerNodeCollector<G, M>()).Members;
        }

        public void PrintPreorder()
        {
            Preorder(new HandlerNodeConsole<G, M>());
        }

        public void PrintPostorder()
        {
            Postorder(new HandlerNodeConsole<G, M>());
        }

        public H Preorder<H>(
            H handler)
            where H : IHandlerNode<G, M>
        {
            handler.Depth = Root.Depth();
            return Root.Preorder(handler);
        }

        public H Postorder<H>(
            H handler)
            where H : IHandlerNode<G, M>
        {
            handler.Depth = Root.Depth();
            return Root.Postorder(handler);
        }
        #endregion

        #region Methods handling members (linear data structure)
        /***********************************************************/
        public void PrintLinear()
        {
            Linear(new HandlerNodeConsole<G, M>());
        }

        public abstract void Linear(IHandlerNode<G, M> handler);
        #endregion
    }
}
