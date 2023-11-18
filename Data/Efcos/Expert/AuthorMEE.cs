using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("author")]
    public class AuthorMEE
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("prename")]
        public string Prename { get; set; }

        [Column("href")]
        public string? Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion
    }
}
