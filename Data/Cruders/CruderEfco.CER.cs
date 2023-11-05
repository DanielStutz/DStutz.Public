using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DStutz.Data.CRUD
{
    public abstract partial class CruderEfco<E>
        //: ICruderDAO
        where E : class
    {
        #region Properties
        /***********************************************************/
        protected DbContext Context { get; }
        protected DbSet<E> Set { get; }
        public int Number { get { return -1; } }
        public bool PrintQuery { get; set; } = false;
        #endregion

        #region Constructors
        /***********************************************************/
        public CruderEfco(
            DbContext context)
        {
            Context = context;
            Set = context.Set<E>();
        }
        #endregion

        #region Methods reading one entity
        /***********************************************************/
        public async ValueTask<E?> FindOrDefault(
            long primaryKey,
            int includeType = CIncludeOLD.All)
        {
            var efco = await Set.FindAsync(primaryKey);

            if (efco == null)
                return null;

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E> FindOrThrow(
            long primaryKey,
            int includeType = CIncludeOLD.All)
        {
            var efco = await Set.FindAsync(primaryKey);

            //if (efco == null)
            //    throw NewNotFoundException<E>(GetType(), primaryKey);

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E?> FindOrDefault(
            object[] primaryKeys,
            int includeType = CIncludeOLD.All)
        {
            var efco = await Set.FindAsync(primaryKeys);

            if (efco == null)
                return null;

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E> FindOrThrow(
            object[] primaryKeys,
            int includeType = CIncludeOLD.All)
        {
            var efco = await Set.FindAsync(primaryKeys);

            //if (efco == null)
            //    throw NewNotFoundException<E>(GetType(), primaryKeys);

            return Loading(Context.Entry(efco), includeType);
        }
        #endregion

        #region Methods reading first entity
        /***********************************************************/
        public async ValueTask<E?> FindFirstOrDefault(
            Expression<Func<E, bool>> predicate,
            int includeType = CIncludeOLD.All)
        {
            var efco = await Set.FirstOrDefaultAsync(predicate);

            if (efco == null)
                return null;

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E> FindFirstOrThrow(
            Expression<Func<E, bool>> predicate,
            int includeType = CIncludeOLD.All)
        {
            var efco = await Set.FirstOrDefaultAsync(predicate);

            //if (efco == null)
            //    throw NewNotFoundException<E>(GetType(), predicate);

            return Loading(Context.Entry(efco), includeType);
        }
        #endregion

        #region Methods reading all entities (selectable)
        /***********************************************************/
        public async ValueTask<List<E>> FindAll(
            IPagination? p)
        {
            return await Loading(Set, 0);

            //var efcos = await Set.ToListAsync();

            //if (efcos == null)
            //    return new List<E>();

            //foreach (var efco in efcos)
            //    Loading(includeType, Context.Entry(efco));

            //return efcos;
        }

        public async ValueTask<List<T>> FindAll<T>(
            Func<E, T> selector,
            IPagination? p)
        {
            var efcos = await FindAll(p);

            return efcos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (selectable)
        /***********************************************************/
        public async ValueTask<List<E>> FindMany(
            Expression<Func<E, bool>> predicate,
            Expression<Func<E, bool>>? predicate2,
            IPagination? p)
        {
            IQueryable<E> queryable = Set;

            queryable = queryable.Where(predicate);

            if (predicate2 != null)
                queryable = queryable.Where(predicate2);

            if (PrintQuery)
                Console.WriteLine(queryable.ToQueryString());

            return await Loading(queryable, 0);

            //var efcos = await
            //    queryable.ToListAsync();

            //if (efcos == null)
            //    return new List<E>();

            //foreach (var efco in efcos)
            //    Loading(Context.Entry(efco), includeType);

            //return efcos;
        }

        public async ValueTask<List<T>> FindMany<T>(
            Expression<Func<E, bool>> predicate,
            Func<E, T> selector,
            IPagination? p)
        {
            var efcos = await FindMany(predicate, null, p);

            return efcos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (by related)
        /***********************************************************/
        public async ValueTask<List<E>> FindMany<R>(
            Expression<Func<R, bool>> predicateOfRelated,
            int includeType = CIncludeOLD.All)
            where R : class, IOwned<E>
        {
            IQueryable<E> queryable =
                Context.Set<R>()
                    .Where(predicateOfRelated)
                    .Select(e => e.Owner!);

            return await Loading(queryable, includeType );
        }
        #endregion

        #region Methods reading owner primary keys of relations
        /***********************************************************/
        [Obsolete("Deprecated, use FindMany<R>(...) instead.")]
        protected Task<List<long>> FindOwnerPrimaryKeys<R>(
            Expression<Func<R, bool>> relatedPredicate)
            where R : class, IRel
        {
            return Context.Set<R>()
                .Where(relatedPredicate)
                .Select(e => e.OwnerPk1)
                .ToListAsync();
        }
        #endregion

        #region Methods loading (explicit)
        /***********************************************************/
        //private E? Loading(
        //    int includeType,
        //    E? efco)
        //{
        //    if (efco != null)
        //        return Loading(includeType, Context.Entry(efco));

        //    return null;
        //}

        //private E Loading(
        //    int includeType,
        //    E efco)
        //{
        //    return Loading(includeType, Context.Entry(efco));
        //}

        // https://docs.microsoft.com/en-us/ef/core/querying/related-data/explicit
        private async ValueTask<List<E>> Loading(
            IQueryable<E> queryable,
            int includeType)
        {
            var efcos = await
                queryable.ToListAsync();

            if (efcos == null)
                return new List<E>();

            foreach (var efco in efcos)
                Loading(Context.Entry(efco), includeType);

            return efcos;
        }

        protected virtual E Loading(
            EntityEntry<E> entry,
            int includeType)
        {
            return entry.Entity;
        }
        #endregion
    }
}
