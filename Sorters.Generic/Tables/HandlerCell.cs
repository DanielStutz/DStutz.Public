namespace DStutz.Sorters.Generic.Tables
{
    /// <summary>
    /// A <c>IHandler</c> can be used to do whatever
    /// with a <c>Member</c> and its <c>Group</c>.
    /// </summary>
    public interface IHandlerCell<G, M>
        where G : IGroupCell<M>, new()
        where M : class
    {
        public void Handle(M member);
        public void Handle(G group);
        public void Handle(G group, M member);
    }

    public abstract class HandlerCell<G, M>
        : IHandlerCell<G, M>
        where G : IGroupCell<M>, new()
        where M : class
    {
        #region Methods implementing
        /***********************************************************/
        public virtual void Handle(M member)
        {
            throw new NotImplementedException();
        }

        public virtual void Handle(G group)
        {
            throw new NotImplementedException();
        }

        public virtual void Handle(G group, M member)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
