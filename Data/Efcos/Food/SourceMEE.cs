using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("source")]
    public class SourceMEE
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("name")]
        public string Name { get; set; }
        #endregion
    }
}
