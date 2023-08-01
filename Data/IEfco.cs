namespace DStutz.Data
{
    public interface IEfco<P>// : IJoinable
    {
        public P Map();
    }

    public interface IEfcoTree<P>// : IJoinable
    {
        public P Map(bool addChildren);
    }
}
