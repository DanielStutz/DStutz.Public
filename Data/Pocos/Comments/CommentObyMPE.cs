using DStutz.System.Joiners;

using DStutz.Data.Efcos.Comments;

// Version 1.1.0
namespace DStutz.Data.Pocos.Comments
{
    public interface ICommentOby
        : IOrdered
    {
        public long Pk1 { get; set; }
        public string Text { get; set; }
    }

    public class CommentObyMPE
        : IPoco<ICommentOby>, ICommentOby
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int OrderBy { get; set; }
        public string Text { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return CommentObyMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : ICommentOby, new()
        {
            return CommentObyMapper.New.Map<E>(this);
        }
        #endregion
    }
}
