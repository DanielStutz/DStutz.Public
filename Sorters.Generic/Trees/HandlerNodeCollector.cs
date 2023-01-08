namespace DStutz.Sorters.Generic.Trees
{
    public class HandlerNodeCollector<G, M>
        : HandlerNode<G, M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        public List<M> Members { get; } = new List<M>();
        #endregion

        #region Methods implementing
        /***********************************************************/
        public override void Handle(G group, M member, int nodeCount)
        {
            Members.Add(member);
        }
        #endregion
    }
}
