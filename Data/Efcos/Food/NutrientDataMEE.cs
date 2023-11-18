using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("nutrient_data")]
    public class NutrientDataMEE
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
    }
}
