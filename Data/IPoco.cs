using DStutz.System.Joiners;

namespace DStutz.Data
{
    public interface IPoco<I> : IJoinable
    {
        public E Map<E>() where E : I, new();
    }
}
