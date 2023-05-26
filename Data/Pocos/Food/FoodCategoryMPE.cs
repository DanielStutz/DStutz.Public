using DStutz.Data.Efcos.Food;

// Version 1.1.0
namespace DStutz.Data.Pocos.Food
{
    public interface IFoodCategory
    {
        public long Pk1 { get; set; }
        public string Name { get; set; }
    }

    public class FoodCategoryMPE
        : IPoco<IFoodCategory>, IFoodCategory
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Name { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return FoodCategoryMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IFoodCategory, new()
        {
            return FoodCategoryMapper.New.Map<E>(this);
        }
        #endregion
    }
}
