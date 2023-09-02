using DStutz.Apps.Services.API;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DStutz.Data.CRUD
{
    public abstract partial class CruderPoco<E, P, I>
        : CruderEfco<E>//, ICruderBLO<P>
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
            params object[] primaryKeys)
        {
            var efco = await FindOrDefault(primaryKeys, 0);

            if (efco == null)
                return null;

            return efco.Map();
        }

        public async ValueTask<P> ReadOrThrow(
            params object[] primaryKeys)
        {
            var efco = await FindOrDefault(primaryKeys, 0);

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
            IPagination? p)
        {
            var efcos = await FindAll(p);

            return efcos.Select(e => e.Map()).ToList();
        }

        public async ValueTask<List<T>> SelectAll<T>(
            Func<P, T> selector,
            IPagination? p)
        {
            var pocos = await ReadAll(p);

            return pocos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (selectable)
        /***********************************************************/
        public async ValueTask<List<P>> ReadMany(
            Expression<Func<E, bool>> predicate,
            IPagination? p)
        {
            var efcos = await FindMany(predicate, null, p);

            return efcos.Select(e => e.Map()).ToList();
        }

        public async ValueTask<List<T>> SelectAll<T>(
            Expression<Func<E, bool>> predicate,
            Func<P, T> selector,
            IPagination? p)
        {
            var pocos = await ReadMany(predicate, p);

            return pocos.Select(e => selector(e)).ToList();
        }

        // TODO Nothing
        public ValueTask<List<P>> ReadMany(
            IEnumerable<object> primaryKeys)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<T>> SelectMany<T>(
            Func<P, T> selector,
            IEnumerable<object> primaryKeys)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<P>> ReadMany(
            IPagination? p,
            DateOnly date1, DateOnly date2)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<P>> ReadMany(
            IPagination? p)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<T>> SelectMany<T>(
            Func<P, T> selector,
            IPagination? p)
        {
            throw new NotImplementedException();
        }

        public List<P> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
