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
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
