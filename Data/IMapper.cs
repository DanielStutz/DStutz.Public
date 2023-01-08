namespace DStutz.Data
{
    public interface IMapper<I>
    {
        public IJoiner Join(I entity, params IJoinable?[] data);
        public E Map<E>(I entity) where E : I, new();
    }
}
