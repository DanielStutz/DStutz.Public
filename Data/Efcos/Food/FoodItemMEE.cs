using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("food_item")]
    public class FoodItemMEE
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

        //#region Relations 1:n (with default foreign key)
        ///***********************************************************/
        //[ForeignKey("Pk1")]
        //public List<NutrientDataMEE> Nutrients { get; set; }
        //#endregion

        //#region Relations m:n (with a junction table)
        ///***********************************************************/
        //public List<FoodItemFoodCategoryRel> CategoryRels { get; set; }
        //#endregion

        #region Asymmetric code (used for initializing from csv)
        /***********************************************************/
        public void AddCategoryRel(
            long categoryPk1)
        {
            //if (CategoryRels == null)
            //    CategoryRels = new();

            //CategoryRels.Add(
            //    new FoodItemFoodCategoryRel()
            //    {
            //        OwnerPk1 = Pk1,
            //        OrderBy = CategoryRels.Count + 1,
            //        RelatedPk1 = categoryPk1
            //    }
            //);
        }

        public void AddDensity(
            string? density)
        {
            if (density != null)
                Density = double.Parse(density);
        }

        //public void AddNutrient(
        //    INutrientValue nutrient)
        //{
        //    if (Nutrients == null)
        //        Nutrients = new();

        //    Nutrients.Add(
        //        new NutrientDataMEE()
        //        {
        //            Pk1 = Pk1,
        //            OrderBy = nutrient.OrderBy,
        //            Value = nutrient.Value,
        //            Derivation = nutrient.Derivation,
        //            Sources = nutrient.Sources,
        //        }
        //    );
        //}
        #endregion
    }

    //[Table("food_item_food_category_rel")]
    //public class FoodItemFoodCategoryRel
    //    : RelEEAny<FoodItemMEE, FoodCategoryMEE, FoodCategoryMPE, IFoodCategory>
    //{
    //    public override IJoiner Joiner
    //    {
    //        get
    //        {
    //            return new Joiner(
    //                ('R', 4, OwnerPk1),
    //                ('R', 3, OrderBy),
    //                ('R', 3, RelatedPk1)
    //            );
    //        }
    //    }
    //}
}
