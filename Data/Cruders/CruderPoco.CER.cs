using DStutz.Apps.Services.API;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DStutz.Data.Cruders
{
    public abstract partial class CruderPoco<E, P, I>
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
        //public async ValueTask<P?> ReadOrDefault(
        //    long primaryKey,
        //    int includeType)
        //{
        //    var efco = await FindOrDefault(primaryKey, includeType);

        //    if (efco == null)
        //        return null;

        //    return efco.Map();
        //}

        //public async ValueTask<P> ReadOrThrow(
        //    long primaryKey,
        //    int includeType)
        //{
        //    var efco = await FindOrDefault(primaryKey, includeType);

        //    if (efco == null)
        //        throw new NotFoundException<E>(primaryKey);

        //    return efco.Map();
        //}

        public async ValueTask<P?> ReadOrDefault(
            int includeType,
            params object[] primaryKeys)
        {
            var efco = await FindOrDefault(primaryKeys, includeType);

            if (efco == null)
                return null;

            return efco.Map();
        }

        public async ValueTask<P> ReadOrThrow(
            int includeType,
            params object[] primaryKeys)
        {
            var efco = await FindOrDefault(primaryKeys, includeType);

            //if (efco == null)
            //    throw NewNotFoundException<E>(GetType(), primaryKeys);

            return efco.Map();
        }
        #endregion

        #region Methods reading first entity
        /***********************************************************/
        public async ValueTask<P?> ReadFirstOrDefault(
            Expression<Func<E, bool>> predicate,
            int includeType)
        {
            var efco = await FindFirstOrDefault(predicate, includeType);

            if (efco == null)
                return null;

            return efco.Map();
        }

        public async ValueTask<P> ReadFirstOrThrow(
            Expression<Func<E, bool>> predicate,
            int includeType)
        {
            var efco = await FindFirstOrDefault(predicate, includeType);

            //if (efco == null)
            //    throw NewNotFoundException<E>(GetType(), predicate);

            return efco.Map();
        }
        #endregion

        #region Methods reading all entities (selectable)
        /***********************************************************/
        public async ValueTask<List<P>> ReadAll(
            int includeType)
        {
            var efcos = await FindAll(includeType);

            return efcos.Select(e => e.Map()).ToList();
        }

        public async ValueTask<List<T>> ReadAll<T>(
            int includeType,
            Func<P, T> selector)
        {
            var pocos = await ReadAll(includeType);

            return pocos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (selectable)
        /***********************************************************/
        public async ValueTask<List<P>> ReadMany(
            Expression<Func<E, bool>> predicate,
            int includeType)
        {
            var efcos = await FindMany(predicate, null, includeType);

            return efcos.Select(e => e.Map()).ToList();
        }

        public async ValueTask<List<T>> ReadMany<T>(
            Expression<Func<E, bool>> predicate,
            Func<P, T> selector,
            int includeType)
        {
            var pocos = await ReadMany(predicate, includeType);

            return pocos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (by related)
        /***********************************************************/
        public async ValueTask<List<P>> ReadMany<R>(
            Expression<Func<R, bool>> predicateOfRelated,
            int includeType)
            where R : class, IOwned<E>
        {
            var efcos = await FindMany(predicateOfRelated, includeType);

            return efcos.Select(e => e.Map()).ToList();
        }

        // TODO Nothing
        public ValueTask<List<P>> ReadMany(
            int includeType,
            DateOnly date1, DateOnly date2)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<P>> ReadMany(
            int includeType,
            ISearchTerm terms)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<T>> ReadMany<T>(
            int includeType,
            ISearchTerm terms,
            Func<P, T> selector)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
