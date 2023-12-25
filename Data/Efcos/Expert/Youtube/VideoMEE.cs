using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Youtube
{
    [Table("youtube_video")]
    public class VideoMEE
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("lang")]
        public string Lang { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("href")]
        public string? Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }

        [Column("identifier")]
        public string Identifier { get; set; }
        #endregion

        //#region Relations 1:n (with default foreign key)
        ///***********************************************************/
        //[ForeignKey("Pk1")]
        //public IReadOnlyList<VideoComment>? Comments { get; set; }
        //#endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("channel_pk1")]
        public long ChannelPk1 { get; set; }

        [ForeignKey("ChannelPk1")]
        public ChannelMEE Channel { get; set; }

        [Column("playlist_pk1")]
        public long? PlaylistPk1 { get; set; }

        [ForeignKey("PlaylistPk1")]
        public PlaylistMEE? Playlist { get; set; }
        #endregion

        //#region Relations m:n (with a junction table)
        ///***********************************************************/
        //public IReadOnlyList<VideoProductRel>? ProductRels { get; set; }
        //public IReadOnlyList<VideoTagRel>? TagRels { get; set; }
        //#endregion
    }

    //[Table("youtube_video_comment")]
    //public class VideoComment
    //: CommentMEE
    //{ }

    //[Table("youtube_video_product_rel")]
    //public class VideoProductRel
    //: RelEEAny<VideoMEE, ProductMEE, ProductMPE, IProduct>
    //{ }

    //[Table("youtube_video_tag_rel")]
    //public class VideoTagRel
    //    : RelEEAny<VideoMEE, TagMEE, TagMPE, ITag>
    //{ }
}
