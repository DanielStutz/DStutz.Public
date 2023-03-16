using DStutz.Data.Pocos;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos
{
    public class TreeNodeMEE<NE, NP, DE, DP>
        : IEfcoTree<NP>, ITreeNode<NE>
        where NE : TreeNodeMEE<NE, NP, DE, DP>
        where NP : TreeNodePE<NP, DP>, new()
        where DE : IEfco<DP>
        where DP : IJoinable
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [NotMapped]
        public int Level { get; set; }
        #endregion

        #region Properties owned
        /***********************************************************/
        public DE Data { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        //[ForeignKey("Pk1")] Recursion does NOT work here!!!
        public IList<NE>? Children { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("parent_pk1")]
        public long? ParentPk1 { get; set; }

        [ForeignKey("ParentPk1")]
        public NE? Parent { get; set; }
        #endregion

        #region Properties and methods implementing
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

        public NP Map(bool addChildren)
        {
            var poco = new NP()
            {
                Pk1 = Pk1,
                Level = Level,
                ParentPk1 = ParentPk1,
                Data = Data.Map()
            };

            if (addChildren && Children != null)
                foreach (var child in Children)
                    poco.Add(child.Map(addChildren));

            return poco;
        }
        #endregion

        #region Methods implementing ITreeNode
        /***********************************************************/
        public void Add(NE child)
        {
            if (Children == null)
                Children = new List<NE>();

            Children.Add(child);
            child.Parent = (NE)this;
        }

        public int CountLevels()
        {
            return TreeNode<NE>.CountLevels((NE)this);
        }

        public int CountNodes()
        {
            return TreeNode<NE>.CountNodes((NE)this);
        }

        public void Print()
        {
            TreeNode<NE>.Print((NE)this);
        }
        #endregion
    }
}
