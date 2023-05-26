namespace DStutz.Data
{
    public interface IEfco<P> : IJoinableOld
    {
        public P Map();
    }

    public interface IEfcoTree<P> : IJoinableOld
    {
        public P Map(bool addChildren);
    }
}
