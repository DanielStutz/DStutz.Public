using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("nutrient_data")]
    public class NutrientDataMEE
        : IEfco<NutrientDataMPE>, INutrientData
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("derivation")]
        public string? Derivation { get; set; }

        [Column("sources")]
        public string? Sources { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return NutrientDataMapper.New.Joiner(this); }
        }

        public NutrientDataMPE Map()
        {
            return NutrientDataMapper.New.Map<NutrientDataMPE>(this);
        }
        #endregion
    }

    public class NutrientDataMapper
        : IMapper<INutrientData>
    {
        public static NutrientDataMapper New { get; } = new NutrientDataMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            INutrientData e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 5, e1.Pk1),
                ('R', 2, e1.OrderBy),
                ('R', 5, e1.Value),
                ('R', 2, e1.Derivation),
                ('R', 25, e1.Sources)
            ).Add(data);
        }

        public E Map<E>(
            INutrientData e1) where E : INutrientData, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                Value = e1.Value,
                Derivation = e1.Derivation,
                Sources = e1.Sources,
            };
        }
        #endregion
    }
}
