﻿using DStutz.Data.Efcos.Food;
using DStutz.Data.Pocos.Food;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.CRUD.Food
{
    public interface ICruderFoodItem
        : ICruderBLO<FoodItemMPE>
    {
        public ValueTask<FoodItemMPE> ReadByName(string name);
        //public ValueTask<List<FoodItemMPE>> ReadManyByCategory(IPagination p, string partialName);
        //public ValueTask<List<FoodItemMPE>> ReadManyByName(IPagination p, string partialName);
        //public ValueTask<List<FoodItemMPE>> ReadManyBySource(IPagination p, int source);
        //public ValueTask<List<FoodItemMPE>> ReadManyBySynonym(IPagination p, string partialSynonym);
    }

    public class CruderFoodItem
        //: CruderPoco<FoodItemMEE, FoodItemMPE, IFoodItem>, ICruderFoodItem
    {
        #region Constructors
        /***********************************************************/
        public CruderFoodItem(
            DbContext context)
            //: base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        //public async ValueTask<FoodItemMPE> ReadByName(
        //    string name)
        //{
        //    return await ReadFirstOrThrow(e =>
        //        e.Name.Equals(name),
        //        CIncludeOLD.All);
        //}

        //public async ValueTask<List<FoodItemMPE>> ReadManyByCategory(
        //    IPagination p,
        //    string partialName)
        //{
        //    return await ReadMany<FoodItemFoodCategoryRel>(
        //        p,
        //        e =>
        //        e.Related != null &&
        //        e.Related.Name.Contains(partialName)
        //    );
        //}

        //public async ValueTask<List<FoodItemMPE>> ReadManyByName(
        //    IPagination p,
        //    string partialName)
        //{
        //    return await ReadMany(
        //        p,
        //        e =>
        //        e.Name.Contains(partialName));
        //}

        //public async ValueTask<List<FoodItemMPE>> ReadManyBySource(
        //    IPagination p,
        //    int source)
        //{
        //    return await ReadMany(
        //        p,
        //        e =>
        //        e.Nutrients.Any(i =>
        //            i.Sources != null &&
        //            i.Sources.Contains(source.ToString())));
        //}

        //public async ValueTask<List<FoodItemMPE>> ReadManyBySynonym(
        //    string partialSynonym)
        //{
        //    return await ReadMany(
        //        p,
        //        e =>
        //        e.Synonyms != null &&
        //        e.Synonyms.Contains(partialSynonym));
        //}
        #endregion

        #region Methods loading
        /***********************************************************/
        protected FoodItemMEE Loading(
            EntityEntry<FoodItemMEE> entry,
            int includeType)
        {
            //switch (includeType)
            //{
            //    case 0:
            //        entry.Collection(e => e.CategoryRels).Query()
            //            .Include(r => r.Related)
            //            .Load();
            //        entry.Collection(e => e.Nutrients)
            //            .Load();
            //        break;
            //    default:
            //        break;
            //}

            return entry.Entity;
        }
        #endregion
    }
}
