using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("nutrient")]
    public class NutrientMEE
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
    }
}
