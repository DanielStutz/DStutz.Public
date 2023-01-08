using DStutz.Data.Printers;

namespace DStutz.Sorters.Generic.Trees
{
    /// <summary>
    /// A <c>IHandler</c> can be used to do whatever
    /// with a <c>Member</c> and its <c>Group</c>.
    /// </summary>
    public interface IHandlerNode<G, M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        public int Depth { get; set; }
        public void Handle(M member);
        public void Handle(G group, int nodeCount);
        public void Handle(G group, M member, int nodeCount);
    }

    public abstract class HandlerNode<G, M>
        : IHandlerNode<G, M>
        where G : IGroupNode<M>, new()
        where M : class
    {
        #region Properties
        /***********************************************************/
        public int Depth { get; set; }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public string Indent(G group)
        {
            return PrinterTree.Indent(Depth, group.Level);
        }

        public virtual void Handle(M member)
        {
            //throw new NotImplementedException();
        }

        public virtual void Handle(G group, int nodeCount)
        {
            //throw new NotImplementedException();
        }

        public virtual void Handle(G group, M member, int nodeCount)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
