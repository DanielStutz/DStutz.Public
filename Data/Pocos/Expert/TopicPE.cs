using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public class TopicPE
        : TreeNodePE<TopicPE, TopicDataMPO>
    { }

    public interface ITopicData
        : IDeEnFr
    {
        public string? Abbr { get; set; }
        public string? Remark { get; set; }
    }

    public class TopicDataMPO
        : IPoco<ITopicData>, ITopicData, IPolyglot
    {
        #region Properties
        /***********************************************************/
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        public string? Abbr { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return TopicDataMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : ITopicData, new()
        {
            return TopicDataMapper.New.Map<E>(this);
        }
        #endregion
    }
}
