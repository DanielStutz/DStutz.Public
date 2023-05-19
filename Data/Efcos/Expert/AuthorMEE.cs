using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("author")]
    public class AuthorMEE
        : IEfco<AuthorMPE>, IAuthor
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("prename")]
        public string Prename { get; set; }

        [Column("href")]
        public string? Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AuthorMapper.New.Joiner(this); }
        }

        public AuthorMPE Map()
        {
            return AuthorMapper.New.Map<AuthorMPE>(this);
        }
        #endregion
    }

    public class AuthorMapper
        : IMapper<IAuthor>
    {
        public static AuthorMapper New { get; } = new AuthorMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IAuthor e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 20, e1.Surname),
                ('L', 20, e1.Prename),
                ('L', 80, e1.Href),
                ('L', 40, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            IAuthor e1) where E : IAuthor, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Surname = e1.Surname,
                Prename = e1.Prename,
                Href = e1.Href,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
