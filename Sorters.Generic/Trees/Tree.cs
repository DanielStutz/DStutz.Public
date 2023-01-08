namespace DStutz.Sorters.Generic.Trees
{
    public class TreeNode<G, M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        public G Group { get; }
        public ICollection<TreeNode<G, M>> Nodes { get { return _children.Values; } }
        #endregion

        #region Fields
        /***********************************************************/
        private Assigner<G, M> _assigner;
        private TreeNode<G, M> _parent;
        private IDictionary<string, TreeNode<G, M>> _children;
        #endregion

        #region Constructors
        /***********************************************************/
        internal TreeNode(
            Assigner<G, M> assigner)
            : this(
                  assigner,
                  assigner.GetComparer(0),
                  assigner.GetRootGroup())
        { }

        private TreeNode(
            Assigner<G, M> assigner,
            M member,
            int level)
            : this(
                  assigner,
                  assigner.GetComparer(level),
                  assigner.GetGroup(member, level))
        { }

        private TreeNode(
            Assigner<G, M> assigner,
            IComparer<string>? comparer,
            G group)
        {
            Group = group;
            _assigner = assigner;
            _children =
                new SortedDictionary<string, TreeNode<G, M>>(comparer);
        }
        #endregion

        #region Methods tree
        /***********************************************************/
        public bool IsRoot()
        {
            return _parent == null;
        }

        public bool IsLeave()
        {
            return _children == null || _children.Count == 0;
        }

        private void Add(
            TreeNode<G, M> node)
        {
            if (node._parent != null)
                node._parent._children.Remove(node.Group.Key);

            node._parent = this;
            _children.Add(node.Group.Key, node);
        }
        #endregion

        #region Methods counting
        /***********************************************************/
        internal int Depth()
        {
            return _assigner.GetLevelCount();
        }

        internal int CountLevels()
        {
            int level = Group.Level;

            foreach (var node in _children.Values)
                level = global::System.Math.Max(level, node.CountLevels());

            return level;
        }

        internal int CountMembers()
        {
            int count = Group.MembersCount;

            foreach (var node in _children.Values)
                count += node.CountMembers();

            return count;
        }

        internal int CountNodes()
        {
            int count = 1;

            foreach (var node in _children.Values)
                count += node.CountNodes();

            return count;
        }
        #endregion

        #region Methods members
        /***********************************************************/
        internal void Add(
            M member)
        {
            if (Group.Level == _assigner.GetLevelCount())
            {
                // The member belongs to this node (last level)
                Group.Add(member);
            }
            else
            {
                // The member belongs to a child node (next level)
                var level = Group.Level + 1;
                var key = _assigner.GetKey(member, level);

                if (!_children.ContainsKey(key))
                    Add(new TreeNode<G, M>(_assigner, member, level));

                _children[key].Add(member);
            }

            _assigner.UpdateGroup(Group, member);
        }

        internal H Preorder<H>(
            H handler)
            where H : IHandlerNode<G, M>
        {
            // Handle group (node info)
            handler.Handle(Group, _children.Count);

            // Handle members from left to right (in order)
            foreach (var member in Group.Members)
                handler.Handle(Group, member, _children.Count);

            // Visit children from left to right (in order)
            foreach (var node in _children.Values)
                node.Preorder(handler);

            return handler;
        }

        internal H Postorder<H>(
            H handler)
            where H : IHandlerNode<G, M>
        {
            // Visit children from right to left (in reverse order)
            foreach (var node in _children.Values.Reverse())
                node.Postorder(handler);

            // Handle group (node info)
            handler.Handle(Group, _children.Count);

            // Handle members from right to left (in reverse order)
            foreach (var member in Group.Members.Reverse())
                handler.Handle(Group, member, _children.Count);

            return handler;
        }
        #endregion
    }
}
