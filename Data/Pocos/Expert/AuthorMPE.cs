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
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Surname { get; set; }
        public string Prename { get; set; }
        public string? Href { get; set; }
        public string? Remark { get; set; }
        #endregion
    }
}
