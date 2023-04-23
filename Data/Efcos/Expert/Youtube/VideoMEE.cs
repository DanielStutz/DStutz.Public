using DStutz.Data.Pocos.Expert;
using DStutz.Data.Pocos.Expert.Youtube;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Youtube
{
    [Table("youtube_video")]
    public class VideoMEE
        : IEfco<VideoMPE>, IVideo
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

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        [ForeignKey("Pk1")]
        public IReadOnlyList<CommentMEE>? Comments { get; set; }
        #endregion

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

        #region Relations m:n (with a junction table)
        /***********************************************************/
        public IReadOnlyList<VideoProductRel>? ProductRels { get; set; }
        public IReadOnlyList<VideoTagRel>? TagRels { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return VideoMapper.New.Joiner(this); }
        }

        public VideoMPE Map()
        {
            return VideoMapper.New.Map<VideoMPE>(this);
        }
        #endregion
    }

    [Table("youtube_video_product_rel")]
    public class VideoProductRel
    : RelEEAny<VideoMEE, ProductMEE, ProductMPE, IProduct>
    { }

    [Table("youtube_video_tag_rel")]
    public class VideoTagRel
        : RelEEAny<VideoMEE, TagMEE, TagMPE, ITag>
    { }

    public class VideoMapper
        : IMapper<IVideo>
    {
        public static VideoMapper New { get; } = new VideoMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IVideo e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 2, e1.Lang),
                ('L', 10, e1.Date),
                ('L', 60, e1.Title),
                ('L', 80, e1.Href),
                ('L', 40, e1.Remark),
                ('L', 11, e1.Identifier),
                ('L', 20, e1.ChannelPk1),
                ('L', 20, e1.PlaylistPk1)
            ).Add(data);
        }

        public E Map<E>(
            IVideo e1) where E : IVideo, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Lang = e1.Lang,
                Date = e1.Date,
                Title = e1.Title,
                Href = e1.Href,
                Remark = e1.Remark,
                Identifier = e1.Identifier,
                ChannelPk1 = e1.ChannelPk1,
                PlaylistPk1 = e1.PlaylistPk1,
            };

            if (typeof(E) == typeof(VideoMEE))
            {
                VideoMPE poco = (VideoMPE)(object)e1;
                VideoMEE efco = (VideoMEE)(object)e2;

                efco.Comments =
                    Mapper.MapOptionals(
                        poco.Comments,
                        e => e.Map<CommentMEE>());

                efco.Channel =
                    Mapper.MapMandatory(
                        poco.Channel,
                        e => e.Map<ChannelMEE>());

                efco.Playlist =
                    Mapper.MapOptional(
                        poco.Playlist,
                        e => e.Map<PlaylistMEE>());

                efco.ProductRels =
                    Mapper.MapOptionals(
                        poco.ProductRels,
                        e => e.Map<VideoProductRel, ProductMEE>());

                efco.TagRels =
                    Mapper.MapOptionals(
                        poco.TagRels,
                        e => e.Map<VideoTagRel, TagMEE>());
            }
            else if (typeof(E) == typeof(VideoMPE))
            {
                VideoMEE efco = (VideoMEE)(object)e1;
                VideoMPE poco = (VideoMPE)(object)e2;

                poco.Comments =
                    Mapper.MapOptionals(
                        efco.Comments,
                        e => e.Map());

                poco.Channel =
                    Mapper.MapMandatory(
                        efco.Channel,
                        e => e.Map());

                poco.Playlist =
                    Mapper.MapOptional(
                        efco.Playlist,
                        e => e.Map());

                poco.ProductRels =
                    Mapper.MapOptionals(
                        efco.ProductRels,
                        e => e.Map());

                poco.TagRels =
                    Mapper.MapOptionals(
                        efco.TagRels,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}
