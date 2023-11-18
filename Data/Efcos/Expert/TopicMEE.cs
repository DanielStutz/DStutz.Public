using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("topic")]
    public class TopicMEE
    { }

    public class TopicDataMEO
    {
        #region Properties
        /***********************************************************/
        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }

        [Column("abbr")]
        public string? Abbr { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion
    }
}
