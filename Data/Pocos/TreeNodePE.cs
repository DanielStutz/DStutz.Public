using System.Text.Json.Serialization;

// Version 1.1.0
namespace DStutz.Data.Pocos
{
    public interface ITreeNode
    {
        public long Pk1 { get; set; }
        public int Level { get; set; }
        public long? ParentPk1 { get; set; }
    }

    public class TreeNodePE<NP, DP>
        : ITreeNode<NP>
        where NP : TreeNodePE<NP, DP>
        where DP : IJoinableOld
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int Level { get; set; }
        #endregion

        #region Properties owned
        /***********************************************************/
        public DP Data { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        public IList<NP>? Children { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long? ParentPk1 { get; set; }

        [JsonIgnore] // Prevent cycles in json
        public NP? Parent { get; set; }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    (20, Pk1),
                    (3, Level),
                    (20, ParentPk1)
                ).Add(Data);
            }
        }
        #endregion

        #region Methods implementing ITreeNode
        /***********************************************************/
        public void Add(NP child)
        {
            if (Children == null)
                Children = new List<NP>();

            Children.Add(child);
            child.Parent = (NP)this;
        }

        public int CountLevels()
        {
            return TreeNode<NP>.CountLevels((NP)this);
        }

        public int CountNodes()
        {
            return TreeNode<NP>.CountNodes((NP)this);
        }

        public void Print()
        {
            TreeNode<NP>.Print((NP)this);
        }
        #endregion
    }
}
