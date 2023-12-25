using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Youtube
{
    [Table("youtube_channel")]
    public class ChannelMEE
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("href")]
        public string? Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }

        [Column("identifier")]
        public string Identifier { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        //[Column("author_pk1")]
        //public long AuthorPk1 { get; set; }

        //[ForeignKey("AuthorPk1")]
        //public AuthorMEE Author { get; set; }
        #endregion
    }
}
