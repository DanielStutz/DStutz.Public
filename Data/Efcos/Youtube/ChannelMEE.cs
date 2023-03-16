using DStutz.Data.Pocos.Youtube;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Youtube
{
    [Table("channel")]
    public class ChannelMEE
        : IEfco<ChannelMPE>, IChannel
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("abbr")]
        public string Abbr { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("website")]
        public string Website { get; set; }

        [Column("person")]
        public string? Person { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }

        [Column("identifier")]
        public string Identifier { get; set; }
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
                ('L', 3, e1.Abbr),
                ('L', 40, e1.Name),
                ('L', 80, e1.Website),
                ('L', 40, e1.Person),
                ('L', 40, e1.Remark),
                ('L', 40, e1.Identifier)
            ).Add(data);
        }

        public E Map<E>(
            IChannel e1) where E : IChannel, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Abbr = e1.Abbr,
                Name = e1.Name,
                Website = e1.Website,
                Person = e1.Person,
                Remark = e1.Remark,
                Identifier = e1.Identifier,
            };
        }
        #endregion
    }
}
