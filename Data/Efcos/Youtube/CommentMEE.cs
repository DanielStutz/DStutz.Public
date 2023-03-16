using DStutz.Data.Pocos.Youtube;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Youtube
{
    [Table("comment")]
    public class CommentMEE
        : IEfco<CommentMPE>, IComment
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return CommentMapper.New.Joiner(this); }
        }

        public CommentMPE Map()
        {
            return CommentMapper.New.Map<CommentMPE>(this);
        }
        #endregion
    }

    public class CommentMapper
        : IMapper<IComment>
    {
        public static CommentMapper New { get; } = new CommentMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IComment e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('R', 3, e1.OrderBy),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR)
            ).Add(data);
        }

        public E Map<E>(
            IComment e1) where E : IComment, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
            };
        }
        #endregion
    }
}
