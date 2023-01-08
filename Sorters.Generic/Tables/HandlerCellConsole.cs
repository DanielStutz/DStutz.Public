namespace DStutz.Sorters.Generic.Tables
{
    public class HandlerCellConsole<G, M>
        : HandlerCell<G, M>
        where G : IGroupCell<M>, new()
        where M : class
    {
        #region Methods implementing
        /***********************************************************/
        public override void Handle(M member)
        {
            Console.WriteLine(member);
        }

        public override void Handle(G group)
        {
            Console.WriteLine(group);
        }

        public override void Handle(G group, M member)
        {
            Console.WriteLine("{0} --> {1}", group, member);
        }
        #endregion
    }
}
