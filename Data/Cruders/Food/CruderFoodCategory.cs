﻿using DStutz.Data.Efcos.Food;
using DStutz.Data.Pocos.Food;

using Microsoft.EntityFrameworkCore;

namespace DStutz.Data.CRUD.Food
{
    public interface ICruderFoodCategory
        : ICruderBLO<FoodCategoryMPE>
    {
        public Task<FoodCategoryMPE> ReadByName(string name);
        public Task<FoodCategoryMPE> ReadByNameCached(string name);
        public Task<List<FoodCategoryMPE>> ReadManyByName(string partialName);
    }

    public class CruderFoodCategory
        : CruderPocoCached<FoodCategoryMEE, FoodCategoryMPE, IFoodCategory, string>, ICruderFoodCategory
    {
        #region Constructors
        /***********************************************************/
        public CruderFoodCategory(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public async Task<FoodCategoryMPE> ReadByName(
            string name)
        {
            return await ReadFirstOrThrow(e =>
                e.Name.Equals(name),
                CInclude.All);
        }

        public async Task<FoodCategoryMPE> ReadByNameCached(
            string name)
        {
            return await ReadFirstOrThrowCached(e =>
                e.Name.Equals(name),
                name,
                CInclude.All);
        }

        public async Task<List<FoodCategoryMPE>> ReadManyByName(
            string partialName)
        {
            return await ReadMany(e =>
                e.Name.Contains(partialName),
                CInclude.All);
        }
        #endregion
    }
}
