using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public interface ITopicData
    {
        public string? Abbr { get; set; }
        public string? Remark { get; set; }
    }

    public class TopicDataMPO
    {
        #region Properties
        /***********************************************************/
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        public string? Abbr { get; set; }
        public string? Remark { get; set; }
        #endregion
    }
}
