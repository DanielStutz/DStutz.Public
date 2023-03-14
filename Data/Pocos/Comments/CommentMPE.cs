using DStutz.System.Joiners;

using DStutz.Data.Efcos.Comments;

// Version 1.1.0
namespace DStutz.Data.Pocos.Comments
{
    public interface IComment
    {
        public long Pk1 { get; set; }
        public string Text { get; set; }
    }

    public class CommentMPE
        : IPoco<IComment>, IComment
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Text { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return CommentMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IComment, new()
        {
            return CommentMapper.New.Map<E>(this);
        }
        #endregion
    }
}
