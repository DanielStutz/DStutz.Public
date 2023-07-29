using DStutz.Data.Efcos.Food;
using DStutz.Data.Pocos.Food;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.CRUD.Food
{
    public interface ICruderFoodItem
        : ICruderBLO<FoodItemMPE>
    {
        public Task<FoodItemMPE> ReadByName(string name);
        public Task<List<FoodItemMPE>> ReadManyByCategory(string partialName);
        public Task<List<FoodItemMPE>> ReadManyByName(string partialName);
        public Task<List<FoodItemMPE>> ReadManyBySource(int source);
        public Task<List<FoodItemMPE>> ReadManyBySynonym(string partialSynonym);
    }

    public class CruderFoodItem
        : CruderPoco<FoodItemMEE, FoodItemMPE, IFoodItem>, ICruderFoodItem
    {
        #region Constructors
        /***********************************************************/
        public CruderFoodItem(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public async Task<FoodItemMPE> ReadByName(
            string name)
        {
            return await ReadFirstOrThrow(e =>
                e.Name.Equals(name),
                CInclude.All);
        }

        public async Task<List<FoodItemMPE>> ReadManyByCategory(
            string partialName)
        {
            return await ReadMany<FoodItemFoodCategoryRel>(e =>
                e.Related != null &&
                e.Related.Name.Contains(partialName),
                CInclude.All
            );
        }

        public async Task<List<FoodItemMPE>> ReadManyByName(
            string partialName)
        {
            return await ReadMany(e =>
                e.Name.Contains(partialName),
                CInclude.All);
        }

        public async Task<List<FoodItemMPE>> ReadManyBySource(
            int source)
        {
            return await ReadMany(e =>
                e.Nutrients.Any(i =>
                    i.Sources != null &&
                    i.Sources.Contains(source.ToString())),
                CInclude.All);
        }

        public async Task<List<FoodItemMPE>> ReadManyBySynonym(
            string partialSynonym)
        {
            return await ReadMany(e =>
                e.Synonyms != null &&
                e.Synonyms.Contains(partialSynonym),
                CInclude.All);
        }
        #endregion

        #region Methods loading
        /***********************************************************/
        protected override FoodItemMEE Loading(
            EntityEntry<FoodItemMEE> entry,
            int includeType)
        {
            switch (includeType)
            {
                case CInclude.All:
                    entry.Collection(e => e.CategoryRels).Query()
                        .Include(r => r.Related)
                        .Load();
                    entry.Collection(e => e.Nutrients)
                        .Load();
                    break;
                default:
                    break;
            }

            return entry.Entity;
        }
        #endregion
    }
}
