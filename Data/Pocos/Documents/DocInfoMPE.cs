using DStutz.Data.Efcos.Documents;

// Version 1.1.0
namespace DStutz.Data.Pocos.Documents
{
    public interface IDocInfo
    {
        public long Pk1 { get; set; }
        public string DocType { get; set; }
        public string DocId { get; set; }
        public string DocDir { get; set; }
        public string DocFile { get; set; }
        public string WorkDir { get; set; }
        public string TempDir { get; set; }
        public string TempFile { get; set; }
        public string DataDir { get; set; }
        public string DataFile { get; set; }
    }

    public class DocInfoMPE
        : IPoco<IDocInfo>, IDocInfo, IJoinable
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string DocType { get; set; }
        public string DocId { get; set; }
        public string DocDir { get; set; }
        public string DocFile { get; set; }
        public string WorkDir { get; set; }
        public string TempDir { get; set; }
        public string TempFile { get; set; }
        public string DataDir { get; set; }
        public string DataFile { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return DocInfoMapper.New.Joiner(this);
        }

        public E Map<E>() where E : IDocInfo, new()
        {
            return DocInfoMapper.New.Map<E>(this);
        }
        #endregion
    }
}
