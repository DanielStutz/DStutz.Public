using System.Linq.Expressions;

namespace DStutz.Data
{
    public interface IEquatableLambda<E> : IJoinable
    {
        public Expression<Func<E, bool>> EqualsLambda();
    }
}
