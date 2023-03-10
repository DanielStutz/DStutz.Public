using DStutz.System.Joiners;

using DStutz.Data.Efcos.People;

// Version 1.1.0
namespace DStutz.Data.Pocos.People
{
    public interface IFeedback
    {
        public long Pk1 { get; set; }
        public FeedbackType Type { get; set; }
        public string Text { get; set; }
    }

    public class FeedbackMPE
        : IPoco<IFeedback>, IFeedback
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public FeedbackType Type { get; set; }
        public string Text { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return FeedbackMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IFeedback, new()
        {
            return FeedbackMapper.New.Map<E>(this);
        }
        #endregion
    }
}
