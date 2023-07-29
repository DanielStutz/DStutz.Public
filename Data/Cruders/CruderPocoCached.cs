using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DStutz.Data.CRUD
{
    public class CruderPocoCached<E, P, I, K>
        : CruderPoco<E, P, I>
        where E : class, I, IEfco<P>, new()
        where P : class, I, IPoco<I>
        where K : notnull
    {
        #region Properties
        /***********************************************************/
        private SortedDictionary<K, P> Pocos { get; } = new();
        #endregion

        #region Constructors
        /***********************************************************/
        public CruderPocoCached(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods reading first entity (cached)
        /***********************************************************/
        public async ValueTask<P> ReadFirstOrThrowCached(
            Expression<Func<E, bool>> predicate,
            K key,
            int includeType = CInclude.All)
        {
            if (!Pocos.ContainsKey(key))
                Pocos.Add(key, await ReadFirstOrThrow(predicate, includeType));

            return Pocos[key];
        }
        #endregion
    }
}
