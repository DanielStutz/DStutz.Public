using System.Linq.Expressions;

namespace DStutz.Data
{
    public interface IEquatableLambda<E>
    {
        public Expression<Func<E, bool>> EqualsLambda();
    }
}
