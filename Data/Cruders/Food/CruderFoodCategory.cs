using DStutz.Data.Efcos.Food;
using DStutz.Data.Pocos.Food;

using Microsoft.EntityFrameworkCore;

namespace DStutz.Data.CRUD.Food
{
    public interface ICruderFoodCategory
        : ICruderBLO<FoodCategoryMPE>
    {
        //public ValueTask<FoodCategoryMPE> ReadByName(string name);
        //public ValueTask<FoodCategoryMPE> ReadByNameCached(string name);
        //public ValueTask<List<FoodCategoryMPE>> ReadManyByName(string partialName);
    }

    public class CruderFoodCategory
        //: CruderPocoCached<FoodCategoryMEE, FoodCategoryMPE, IFoodCategory, string>, ICruderFoodCategory
    {
        #region Constructors
        /***********************************************************/
        public CruderFoodCategory(
            DbContext context)
            //: base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        //public async ValueTask<FoodCategoryMPE> ReadByName(
        //    string name)
        //{
        //    return await ReadFirstOrThrow(e =>
        //        e.Name.Equals(name),
        //        CInclude.All);
        //}

        //public async ValueTask<FoodCategoryMPE> ReadByNameCached(
        //    string name)
        //{
        //    return await ReadFirstOrThrowCached(e =>
        //        e.Name.Equals(name),
        //        name,
        //        CInclude.All);
        //}

        //public async ValueTask<List<FoodCategoryMPE>> ReadManyByName(
        //    string partialName)
        //{
        //    return await ReadMany(e =>
        //        e.Name.Contains(partialName),
        //        CInclude.All);
        //}
        #endregion
    }
}
