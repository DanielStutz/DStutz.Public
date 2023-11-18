using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("producer")]
    public class ProducerMEE
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("abbr")]
        public string Abbr { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("country")]
        public string Country { get; set; }
        #endregion
    }
}
