using DStutz.Data.Pocos.Expert.Youtube;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Youtube
{
    [Table("youtube_playlist")]
    public class PlaylistMEE
        : IEfco<PlaylistMPE>, IPlaylist
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

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return PlaylistMapper.New.Joiner(this); }
        }

        public PlaylistMPE Map()
        {
            return PlaylistMapper.New.Map<PlaylistMPE>(this);
        }
        #endregion
    }

    public class PlaylistMapper
        : IMapper<IPlaylist>
    {
        public static PlaylistMapper New { get; } = new PlaylistMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IPlaylist e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 60, e1.Title),
                ('L', 40, e1.Remark),
                ('L', 34, e1.Identifier),
                ('L', 20, e1.ChannelPk1)
            ).Add(data);
        }

        public E Map<E>(
            IPlaylist e1) where E : IPlaylist, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Title = e1.Title,
                Remark = e1.Remark,
                Identifier = e1.Identifier,
                ChannelPk1 = e1.ChannelPk1,
            };

            if (typeof(E) == typeof(PlaylistMEE))
            {
                PlaylistMPE poco = (PlaylistMPE)(object)e1;
                PlaylistMEE efco = (PlaylistMEE)(object)e2;

                efco.Channel =
                    Mapper.MapMandatory(
                        poco.Channel,
                        e => e.Map<ChannelMEE>());
            }
            else if (typeof(E) == typeof(PlaylistMPE))
            {
                PlaylistMEE efco = (PlaylistMEE)(object)e1;
                PlaylistMPE poco = (PlaylistMPE)(object)e2;

                poco.Channel =
                    Mapper.MapMandatory(
                        efco.Channel,
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
