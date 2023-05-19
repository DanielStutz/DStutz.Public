using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("food_category")]
    public class FoodCategoryMEE
        : IEfco<FoodCategoryMPE>, IFoodCategory
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return FoodCategoryMapper.New.Joiner(this); }
        }

        public FoodCategoryMPE Map()
        {
            return FoodCategoryMapper.New.Map<FoodCategoryMPE>(this);
        }
        #endregion
    }

    public class FoodCategoryMapper
        : IMapper<IFoodCategory>
    {
        public static FoodCategoryMapper New { get; } = new FoodCategoryMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IFoodCategory e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 3, e1.Pk1),
                ('L', 80, e1.Name)
            ).Add(data);
        }

        public E Map<E>(
            IFoodCategory e1) where E : IFoodCategory, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Name = e1.Name,
            };
        }
        #endregion
    }
}
