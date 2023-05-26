using System.Linq.Expressions;

namespace DStutz.Data
{
    public interface IEquatableLambda<E> : IJoinableOld
    {
        public Expression<Func<E, bool>> EqualsLambda();
    }
}
