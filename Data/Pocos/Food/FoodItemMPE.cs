using DStutz.Data.Efcos.Food;

// Version 1.1.0
namespace DStutz.Data.Pocos.Food
{
    public interface IFoodItem
    {
        public long Pk1 { get; set; }
        public int? IdV40 { get; set; }
        public int? IdSwissFIR { get; set; }
        public string Name { get; set; }
        public string? Synonyms { get; set; }
        public double? Density { get; set; }
        public double Energy { get; set; } // kJ
        public string ReferenceUnit { get; set; }
        public bool ChangedEntry { get; set; }
    }

    public class FoodItemMPE
        : IPoco<IFoodItem>, IFoodItem
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int? IdV40 { get; set; }
        public int? IdSwissFIR { get; set; }
        public string Name { get; set; }
        public string? Synonyms { get; set; }
        public double? Density { get; set; }
        public double Energy { get; set; } // kJ
        public string ReferenceUnit { get; set; }
        public bool ChangedEntry { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        public IReadOnlyList<NutrientDataMPE> Nutrients { get; set; }
        #endregion

        #region Relations m:n (with a junction table)
        /***********************************************************/
        public IReadOnlyList<RelPEAny<FoodCategoryMPE, IFoodCategory>> CategoryRels { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        [JsonIgnore]
        public IJoiner Joiner
        {
            get
            {
                var nutrients = string.Join(
                    " | ",
                    Nutrients.Select(e =>
                        new Joiner(
                            " ",
                            ('R', 2, e.OrderBy),
                            ('R', 5, e.Value),
                            ('R', 2, e.Derivation),
                            ('R', 8, e.Sources)
                        )
                    .Row)
                );

                return FoodItemMapper.New.Joiner(this)
                    .Add(('L', nutrients));
            }
        }

        public E Map<E>() where E : IFoodItem, new()
        {
            return FoodItemMapper.New.Map<E>(this);
        }
        #endregion
    }
}
