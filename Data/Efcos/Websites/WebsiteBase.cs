using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DStutz.Data.Efcos.Websites
{
    [Table("website_base")]
    public class WebsiteBase
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("identifier")]
        public string Identifier { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion
    }
}
