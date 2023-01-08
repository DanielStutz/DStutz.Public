namespace DStutz.Sorters.Generic.Trees
{
    public class HandlerNodeConsole<G, M>
        : HandlerNode<G, M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        #region Methods implementing
        /***********************************************************/
        // Called for the linear data stucture only
        public override void Handle(M member)
        {
            Console.WriteLine(
                member);
        }

        // Called for the tree data stucture
        public override void Handle(G group, int nodeCount)
        {
            Console.WriteLine(
                Indent(group) +
                group.ToStringGroup() +
                string.Format(
                    " | Ch = {0,3}",
                    nodeCount));
        }

        // Called for the tree data stucture
        public override void Handle(G group, M member, int nodeCount)
        {
            Console.WriteLine(
                "    " +
                Indent(group) +
                member);
        }
        #endregion
    }
}
