using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("food_item")]
    public class FoodItemMEE
        : IEfco<FoodItemMPE>, IFoodItem
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("id_v40")]
        public int? IdV40 { get; set; }

        [Column("id_swissfir")]
        public int? IdSwissFIR { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("synonyms")]
        public string? Synonyms { get; set; }

        [Column("density")]
        public double? Density { get; set; }

        [Column("energy_kj")]
        public double Energy { get; set; } // kJ

        [Column("reference_unit")]
        public string ReferenceUnit { get; set; }

        [Column("changed")]
        public bool ChangedEntry { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        [ForeignKey("Pk1")]
        public List<NutrientDataMEE> Nutrients { get; set; }
        #endregion

        #region Relations m:n (with a junction table)
        /***********************************************************/
        public List<FoodItemFoodCategoryRel> CategoryRels { get; set; }
        #endregion

        #region Asymmetric code (used for initializing from csv)
        /***********************************************************/
        public void AddCategoryRel(
            long categoryPk1)
        {
            if (CategoryRels == null)
                CategoryRels = new();

            CategoryRels.Add(
                new FoodItemFoodCategoryRel()
                {
                    OwnerPk1 = Pk1,
                    OrderBy = CategoryRels.Count + 1,
                    RelatedPk1 = categoryPk1
                }
            );
        }

        public void AddDensity(
            string? density)
        {
            if (density != null)
                Density = double.Parse(density);
        }

        public void AddNutrient(
            INutrientValue nutrient)
        {
            if (Nutrients == null)
                Nutrients = new();

            Nutrients.Add(
                new NutrientDataMEE()
                {
                    Pk1 = Pk1,
                    OrderBy = nutrient.OrderBy,
                    Value = nutrient.Value,
                    Derivation = nutrient.Derivation,
                    Sources = nutrient.Sources,
                }
            );
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return FoodItemMapper.New.Joiner(this); }
        }

        public FoodItemMPE Map()
        {
            return FoodItemMapper.New.Map<FoodItemMPE>(this);
        }
        #endregion
    }

    [Table("food_item_food_category_rel")]
    public class FoodItemFoodCategoryRel
        : RelEEAny<FoodItemMEE, FoodCategoryMEE, FoodCategoryMPE, IFoodCategory>
    {
        public override IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    ('R', 4, OwnerPk1),
                    ('R', 3, OrderBy),
                    ('R', 3, RelatedPk1)
                );
            }
        }
    }

    public class FoodItemMapper
        : IMapper<IFoodItem>
    {
        public static FoodItemMapper New { get; } = new FoodItemMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IFoodItem e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 5, e1.Pk1),
                ('R', 4, e1.IdV40),
                ('R', 7, e1.IdSwissFIR),
                ('L', 60, e1.Name),
                ('L', 20, e1.Synonyms),
                ('R', 4, e1.Density),
                ('R', 7, e1.Energy.ToString("N1")),
                ('R', 5, e1.ReferenceUnit),
                ('R', 5, e1.ChangedEntry)
            ).Add(data);
        }

        public E Map<E>(
            IFoodItem e1) where E : IFoodItem, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                IdV40 = e1.IdV40,
                IdSwissFIR = e1.IdSwissFIR,
                Name = e1.Name,
                Synonyms = e1.Synonyms,
                Density = e1.Density,
                Energy = e1.Energy,
                ReferenceUnit = e1.ReferenceUnit,
                ChangedEntry = e1.ChangedEntry,
            };

            if (typeof(E) == typeof(FoodItemMEE))
            {
                FoodItemMPE poco = (FoodItemMPE)(object)e1;
                FoodItemMEE efco = (FoodItemMEE)(object)e2;

                efco.Nutrients =
                    Mapper.MapMandatories(
                        poco.Nutrients,
                        e => e.Map<NutrientDataMEE>());

                efco.CategoryRels =
                    Mapper.MapMandatories(
                        poco.CategoryRels,
                        e => e.Map<FoodItemFoodCategoryRel, FoodCategoryMEE>());
            }
            else if (typeof(E) == typeof(FoodItemMPE))
            {
                FoodItemMEE efco = (FoodItemMEE)(object)e1;
                FoodItemMPE poco = (FoodItemMPE)(object)e2;

                poco.Nutrients =
                    Mapper.MapMandatories(
                        efco.Nutrients,
                        e => e.Map());

                poco.CategoryRels =
                    Mapper.MapMandatories(
                        efco.CategoryRels,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}
