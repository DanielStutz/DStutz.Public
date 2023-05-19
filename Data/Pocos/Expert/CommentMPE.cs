using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public interface IComment
        : IOrdered
    {
        public long Pk1 { get; set; }
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
    }

    public class CommentMPE
        : IPoco<IComment>, IComment
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int OrderBy { get; set; }
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        [JsonIgnore]
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
