using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("nutrient")]
    public class NutrientMEE
        : IEfco<INutrientMPE>, INutrient
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("group_pk1")]
        public long Group { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return NutrientMapper.New.Joiner(this); }
        }

        public INutrientMPE Map()
        {
            return NutrientMapper.New.Map<INutrientMPE>(this);
        }
        #endregion
    }

    public class NutrientMapper
        : IMapper<INutrient>
    {
        public static NutrientMapper New { get; } = new NutrientMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            INutrient e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 2, e1.Pk1),
                ('L', 40, e1.DE),
                ('L', 40, e1.EN),
                ('L', 40, e1.FR),
                ('L', 6, e1.Unit),
                ('R', 2, e1.Group)
            ).Add(data);
        }

        public E Map<E>(
            INutrient e1) where E : INutrient, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
                Unit = e1.Unit,
                Group = e1.Group,
            };
        }
        #endregion
    }
}
