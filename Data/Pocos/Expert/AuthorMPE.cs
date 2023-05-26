using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public interface IAuthor
    {
        public long Pk1 { get; set; }
        public string Surname { get; set; }
        public string Prename { get; set; }
        public string? Href { get; set; }
        public string? Remark { get; set; }
    }

    public class AuthorMPE
        : IPoco<IAuthor>, IAuthor
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Surname { get; set; }
        public string Prename { get; set; }
        public string? Href { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AuthorMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IAuthor, new()
        {
            return AuthorMapper.New.Map<E>(this);
        }
        #endregion
    }
}
