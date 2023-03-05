using DStutz.System.Joiners;

using DStutz.Data.Pocos.Documents;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1
namespace DStutz.Data.Efcos.Documents
{
    [Table("doc_info")]
    public class DocInfoMEE
        : IEfco<DocInfoMPE>, IDocInfo
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("doc_type")]
        public string DocType { get; set; }

        [Column("doc_id")]
        public string DocId { get; set; }

        [Column("doc_dir")]
        public string DocDir { get; set; }

        [Column("doc_file")]
        public string DocFile { get; set; }

        [Column("work_dir")]
        public string WorkDir { get; set; }

        [Column("temp_dir")]
        public string TempDir { get; set; }

        [Column("temp_file")]
        public string TempFile { get; set; }

        [Column("data_dir")]
        public string DataDir { get; set; }

        [Column("data_file")]
        public string DataFile { get; set; }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return DocInfoMapper.New.Joiner(this);
        }

        public DocInfoMPE Map()
        {
            return DocInfoMapper.New.Map<DocInfoMPE>(this);
        }
        #endregion
    }

    public class DocInfoMapper
        : IMapper<IDocInfo>
    {
        public static DocInfoMapper New { get; } = new DocInfoMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IDocInfo e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 2, e1.Pk1),
                ('L', 40, e1.DocType + "/" + e1.DocId),
                ('L', 50, e1.DocDir + "/" + e1.DocFile),
                ('L', 50, e1.WorkDir + "/" + e1.TempDir + "/" + e1.TempFile),
                ('L', 50, e1.WorkDir + "/" + e1.DataDir + "/" + e1.DataFile)
            ).Add(data);
        }

        public E Map<E>(
            IDocInfo e1) where E : IDocInfo, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                DocType = e1.DocType,
                DocId = e1.DocId,
                DocDir = e1.DocDir,
                DocFile = e1.DocFile,
                WorkDir = e1.WorkDir,
                TempDir = e1.TempDir,
                TempFile = e1.TempFile,
                DataDir = e1.DataDir,
                DataFile = e1.DataFile,
            };
        }
        #endregion
    }
}
