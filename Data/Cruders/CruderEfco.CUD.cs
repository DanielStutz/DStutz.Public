using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.CRUD
{
    public abstract partial class CruderEfco<E>
    {
        #region Properties
        /***********************************************************/
        public bool PrintFindOrCreate { get; set; } = false;
        #endregion

        #region Methods creating
        /***********************************************************/
        public async ValueTask<E> Create(
            E efco,
            bool saveChanges)
        {
            return (await CreateEntry(efco, saveChanges)).Entity;
        }

        public async ValueTask<EntityEntry<E>> CreateEntry(
            E efco,
            bool saveChanges = false)
        {
            var entry = await Set.AddAsync(efco);

            if (saveChanges)
                await Context.SaveChangesAsync();

            return entry;
        }
        #endregion

        #region Methods finding or creating
        /***********************************************************/
        public async ValueTask<E> FindOrCreate<T>(
            T efco,
            int includeType,
            bool saveChanges)
            where T : E, IEquatableLambda<E>
        {
            var state = "";
            var entity = await FindFirstOrDefault(efco.EqualsLambda(), includeType);

            if (entity != null)
            {
                state = "Old entry";
                //Set.Remove(efco);
            }
            else
            {
                state = "New entry";
                entity = await Create(efco, saveChanges);
            }

            // TODO
            if (entity == null)
                throw new Exception("Unable to create entity");

            if (PrintFindOrCreate &&
                entity != null &&
                entity is IJoinableOld)
                Console.WriteLine(
                    state +
                    " --> " +
                    (entity as IJoinableOld).Joiner.Row);

            return entity;
        }
        #endregion

        #region Methods updating
        /***********************************************************/
        public async ValueTask<E> Update(
            E efco,
            bool saveChanges = true)
        {
            return (await UpdateEntry(efco, saveChanges)).Entity;
        }

        public async ValueTask<EntityEntry<E>> UpdateEntry(
            E efco,
            bool saveChanges = true)
        {
            // What's the difference?!
            // Context.Entry(efco).State = EntityState.Modified;
            var entry = Set.Update(efco);

            if (saveChanges)
                await Context.SaveChangesAsync();

            return entry;
        }
        #endregion

        #region Methods deleting one entity
        /***********************************************************/
        public async ValueTask<int> Delete(
            long primaryKey,
            bool saveChanges)
        {
            var efco = await FindOrThrow(primaryKey);

            return await Delete(efco, saveChanges);
        }

        public async ValueTask<int> Delete(
            bool saveChanges,
            object[] primaryKeys)
        {
            var efco = await FindOrThrow(primaryKeys);

            return await Delete(efco, saveChanges);
        }

        public async ValueTask<int> Delete(
            E efco,
            bool saveChanges = true)
        {
            Set.Remove(efco);

            if (saveChanges)
                return await Context.SaveChangesAsync();

            return 0;
        }

        public async ValueTask<EntityEntry<E>> DeleteEntry(
            E efco,
            bool saveChanges = true)
        {
            var entry = Set.Remove(efco);

            if (saveChanges)
                await Context.SaveChangesAsync();

            return entry;
        }
        #endregion

        #region Methods deleting all entities
        /***********************************************************/
        public async ValueTask<int> DeleteAll(
            bool saveChanges = true)
        {
            foreach (var efco in Set)
                Set.Remove(efco);

            if (saveChanges)
                return await Context.SaveChangesAsync();

            return 0;
        }
        #endregion
    }
}
