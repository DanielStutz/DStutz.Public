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
        public E Create(
            E efco,
            bool saveChanges)
        {
            throw new NotImplementedException();
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
