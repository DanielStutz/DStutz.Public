namespace DStutz.Data
{
    public interface IPoco<I>// : IJoinableOld
    {
        public E Map<E>() where E : I, new();
    }
}
