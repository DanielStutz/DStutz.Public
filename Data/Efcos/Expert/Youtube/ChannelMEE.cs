using DStutz.Data.Pocos.Expert.Youtube;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Youtube
{
    [Table("youtube_channel")]
    public class ChannelMEE
        : IEfco<ChannelMPE>, IChannel
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
        [Column("author_pk1")]
        public long AuthorPk1 { get; set; }

        [ForeignKey("AuthorPk1")]
        public AuthorMEE Author { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ChannelMapper.New.Joiner(this); }
        }

        public ChannelMPE Map()
        {
            return ChannelMapper.New.Map<ChannelMPE>(this);
        }
        #endregion
    }

    public class ChannelMapper
        : IMapper<IChannel>
    {
        public static ChannelMapper New { get; } = new ChannelMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IChannel e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 60, e1.Name),
                ('L', 80, e1.Href),
                ('L', 40, e1.Remark),
                ('L', 24, e1.Identifier),
                ('L', 20, e1.AuthorPk1)
            ).Add(data);
        }

        public E Map<E>(
            IChannel e1) where E : IChannel, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Name = e1.Name,
                Href = e1.Href,
                Remark = e1.Remark,
                Identifier = e1.Identifier,
                AuthorPk1 = e1.AuthorPk1,
            };

            if (typeof(E) == typeof(ChannelMEE))
            {
                ChannelMPE poco = (ChannelMPE)(object)e1;
                ChannelMEE efco = (ChannelMEE)(object)e2;

                efco.Author =
                    Mapper.MapMandatory(
                        poco.Author,
                        e => e.Map<AuthorMEE>());
            }
            else if (typeof(E) == typeof(ChannelMPE))
            {
                ChannelMEE efco = (ChannelMEE)(object)e1;
                ChannelMPE poco = (ChannelMPE)(object)e2;

                poco.Author =
                    Mapper.MapMandatory(
                        efco.Author,
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
