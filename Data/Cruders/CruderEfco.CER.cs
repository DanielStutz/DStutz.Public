using DStutz.Coder.Entities.Data;
using DStutz.System.Exceptions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DStutz.Data.Cruders
{
    public abstract partial class CruderEfco<E>
        : ICruderDAO
        where E : class
    {
        #region Properties
        /***********************************************************/
        protected DbContext Context { get; }
        protected DbSet<E> Set { get; }
        public string Name { get; }
        public int Number { get { return Count(); } }
        public bool PrintQuery { get; set; } = false;
        #endregion

        #region Constructors
        /***********************************************************/
        public CruderEfco(
            DbContext context)
        {
            Context = context;
            Set = context.Set<E>();
            Name = DataType.Name<E>();
        }
        #endregion

        #region Methods counting and existing
        /***********************************************************/
        public int Count()
        {
            // No AnyAsync?!
            return Set.Count();
        }

        public int Count(
            Expression<Func<E, bool>> predicate)
        {
            // No AnyAsync?!
            return Set.Count(predicate);
        }

        public bool Exists(
            long primaryKey)
        {
            // Use FindAsync ?!
            return Set.Find(primaryKey) != null;
        }

        public bool Exists(
            object[] primaryKeys)
        {
            // Use FindAsync ?!
            return Set.Find(primaryKeys) != null;
        }

        public bool Exists(
            Expression<Func<E, bool>> predicate)
        {
            // No AnyAsync ?!
            return Set.Any(predicate);
        }
        #endregion

        #region Methods reading one entity
        /***********************************************************/
        public async ValueTask<E?> FindOrDefault(
            long primaryKey,
            int includeType = CInclude.All)
        {
            var efco = await Set.FindAsync(primaryKey);

            if (efco == null)
                return null;

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E> FindOrThrow(
            long primaryKey,
            int includeType = CInclude.All)
        {
            var efco = await Set.FindAsync(primaryKey);

            if (efco == null)
                throw new NotFoundException<E>(primaryKey);

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E?> FindOrDefault(
            object[] primaryKeys,
            int includeType = CInclude.All)
        {
            var efco = await Set.FindAsync(primaryKeys);

            if (efco == null)
                return null;

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E> FindOrThrow(
            object[] primaryKeys,
            int includeType = CInclude.All)
        {
            var efco = await Set.FindAsync(primaryKeys);

            if (efco == null)
                throw new NotFoundException<E>(primaryKeys);

            return Loading(Context.Entry(efco), includeType);
        }
        #endregion

        #region Methods reading first entity
        /***********************************************************/
        public async ValueTask<E?> FindFirstOrDefault(
            Expression<Func<E, bool>> predicate,
            int includeType = CInclude.All)
        {
            var efco = await Set.FirstOrDefaultAsync(predicate);

            if (efco == null)
                return null;

            return Loading(Context.Entry(efco), includeType);
        }

        public async ValueTask<E> FindFirstOrThrow(
            Expression<Func<E, bool>> predicate,
            int includeType = CInclude.All)
        {
            var efco = await Set.FirstOrDefaultAsync(predicate);

            if (efco == null)
                throw new NotFoundException<E>(predicate);

            return Loading(Context.Entry(efco), includeType);
        }
        #endregion

        #region Methods reading all entities (selectable)
        /***********************************************************/
        public async ValueTask<List<E>> FindAll(
            int includeType = CInclude.All)
        {
            return await Loading(Set, includeType);

            //var efcos = await Set.ToListAsync();

            //if (efcos == null)
            //    return new List<E>();

            //foreach (var efco in efcos)
            //    Loading(includeType, Context.Entry(efco));

            //return efcos;
        }

        public async ValueTask<List<T>> FindAll<T>(
            Func<E, T> selector,
            int includeType = CInclude.All)
        {
            var efcos = await FindAll(includeType);

            return efcos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (selectable)
        /***********************************************************/
        public async ValueTask<List<E>> FindMany(
            Expression<Func<E, bool>> predicate,
            Expression<Func<E, bool>>? predicate2,
            int includeType = CInclude.All)
        {
            IQueryable<E> queryable = Set;

            queryable = queryable.Where(predicate);

            if (predicate2 != null)
                queryable = queryable.Where(predicate2);

            if (PrintQuery)
                Console.WriteLine(queryable.ToQueryString());

            return await Loading(queryable, includeType);

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
            int includeType = CInclude.All)
        {
            var efcos = await FindMany(predicate, null, includeType);

            return efcos.Select(e => selector(e)).ToList();
        }
        #endregion

        #region Methods reading many entities (by related)
        /***********************************************************/
        public async ValueTask<List<E>> FindMany<R>(
            Expression<Func<R, bool>> predicateOfRelated,
            int includeType = CInclude.All)
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
