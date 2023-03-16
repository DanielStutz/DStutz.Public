using DStutz.Data.Pocos;

namespace DStutz.Data
{
    public interface ITreeNode<T>
        : ITreeNode, IJoinable
        where T : ITreeNode<T>
    {
        public T? Parent { get; }
        public IList<T>? Children { get; }
        public void Add(T child);

        public int CountLevels();
        public int CountNodes();
        public void Print();
    }
}
