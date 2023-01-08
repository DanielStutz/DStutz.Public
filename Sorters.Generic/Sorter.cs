namespace DStutz.Sorters.Generic
{
    public abstract class Sorter<M>
        where M : class
    {
        public ICollection<string> Errors { get; } = new List<string>();
    }
}
