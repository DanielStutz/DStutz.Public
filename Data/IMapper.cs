namespace DStutz.Data
{
    public interface IMapper<I>
    {
        public IJoiner Joiner(I entity, params IJoinableOld?[] data);
        public E Map<E>(I entity) where E : I, new();
    }
}
