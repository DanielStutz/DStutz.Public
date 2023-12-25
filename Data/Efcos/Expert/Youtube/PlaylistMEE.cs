
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Youtube
{
    [Table("youtube_playlist")]
    public class PlaylistMEE
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }

        [Column("identifier")]
        public string Identifier { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("channel_pk1")]
        public long ChannelPk1 { get; set; }

        [ForeignKey("ChannelPk1")]
        public ChannelMEE Channel { get; set; }
        #endregion
    }
}
