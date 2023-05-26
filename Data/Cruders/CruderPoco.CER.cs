using DStutz.System.Exceptions;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DStutz.Data.Cruders
{
    public partial class CruderPoco<E, P, I>
        : CruderEfco<E>, ICruderBLO<P>
        where E : class, I, IEfco<P>, new()
        where P : class, I, IPoco<I>
    {
        #region Constructors
        /***********************************************************/
        public CruderPoco(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods reading one entity
        /***********************************************************/
        public async ValueTask<P?> ReadOrDefault(
            long primaryKey,
            int includeType = CInclude.All)
        {
            var efco = await FindOrDefault(primaryKey, includeType);

            if (efco == null)
                return null;

            return efco.Map();
        }

        public async ValueTask<P> ReadOrThrow(
            long primaryKey,
            int includeType = CInclude.All)
        {
            var efco = await FindOrDefault(primaryKey, includeType);

            if (efco == null)
                throw new NotFoundException<E>(primaryKey);

            return efco.Map();
        }

        public async ValueTask<P?> ReadOrDefault(
            object[] primaryKeys,
            int includeType = CInclude.All)
        {
            var efco = await FindOrDefault(primaryKeys, includeType);

            if (efco == null)
                return null;

            return efco.Map();
        }

        public async ValueTask<P> ReadOrThrow(
            object[] primaryKeys,
            int includeType = CInclude.All)
        {
            var efco = await FindOrDefault(primaryKeys, includeType);

            if (efco == null)
                throw new NotFoundException<E>(primaryKeys);

            return efco.Map();
        }
        #endregion

        #region Methods reading first entity
        /***********************************************************/
        public async ValueTask<P?> ReadFirstOrDefault(
            Expression<Func<E, bool>> predicate,
            int includeType = CInclude.All)
        {
            var efco = await FindFirstOrDefault(predicate, includeType);

            if (efco == null)
                return null;

            return efco.Map();
        }

        public async ValueTask<P> ReadFirstOrThrow(
            Expression<Func<E, bool>> predicate,
            int includeType = CInclude.All)
        {
            var efco = await FindFirstOrDefault(predicate, includeType);

            if (efco == null)
                throw new NotFoundException<E>(predicate);

            return efco.Map();
        }
        #endregion

        #region Methods reading all entities (selectable)
        /***********************************************************/
        public async Task<List<P>> ReadAll(
            int includeType = CInclude.All)
        {
            var efcos = await FindAll(includeType);

            return efcos.Select(e => e.Map()).ToList();
        }

        public async Task<List<T>> ReadAll<T>(
            Func<P, T> selector,
            int includeType = CInclude.All)
        {
            var pocos = await ReadAll(includeType);

            return pocos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (selectable)
        /***********************************************************/
        public async Task<List<P>> ReadMany(
            Expression<Func<E, bool>> predicate,
            int includeType = CInclude.All)
        {
            var efcos = await FindMany(predicate, null, includeType);

            return efcos.Select(e => e.Map()).ToList();
        }

        public async Task<List<T>> ReadMany<T>(
            Expression<Func<E, bool>> predicate,
            Func<P, T> selector,
            int includeType = CInclude.All)
        {
            var pocos = await ReadMany(predicate, includeType);

            return pocos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (by related)
        /***********************************************************/
        public async Task<List<P>> ReadMany<R>(
            Expression<Func<R, bool>> predicateOfRelated,
            int includeType = CInclude.All)
            where R : class, IOwned<E>
        {
            var efcos = await FindMany(predicateOfRelated, includeType);

            return efcos.Select(e => e.Map()).ToList();
        }
        #endregion
    }
}
