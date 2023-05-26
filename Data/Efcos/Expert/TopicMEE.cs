using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("topic")]
    public class TopicMEE
        : TreeNodeMEE<TopicMEE, TopicPE, TopicDataMEO, TopicDataMPO>
    { }

    public class TopicDataMEO
        : IEfco<TopicDataMPO>, ITopicData
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

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return TopicDataMapper.New.Joiner(this); }
        }

        public TopicDataMPO Map()
        {
            return TopicDataMapper.New.Map<TopicDataMPO>(this);
        }
        #endregion
    }

    public class TopicDataMapper
        : IMapper<ITopicData>
    {
        public static TopicDataMapper New { get; } = new TopicDataMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            ITopicData e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR),
                ('L', 3, e1.Abbr),
                ('L', 20, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            ITopicData e1) where E : ITopicData, new()
        {
            return new E()
            {
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
                Abbr = e1.Abbr,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
