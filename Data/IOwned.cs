namespace DStutz.Data
{
    public interface IOwned<O>
    {
        public O? Owner { get; set; }
    }
}
