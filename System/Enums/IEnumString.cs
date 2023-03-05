namespace DStutz.System.Enums
{
    public interface IEnumString<E>
    {
        public E Map(string? value);
        public string Map(E value);
    }
}
