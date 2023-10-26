using DStutz.Data.Pocos.Comments;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Comments
{
    public abstract class CommentObyMEE
        : IEfco<CommentObyMPE>, ICommentOby
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("text")]
        public string Text { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return CommentObyMapper.New.Joiner(this); }
        }

        public CommentObyMPE Map()
        {
            return CommentObyMapper.New.Map<CommentObyMPE>(this);
        }
        #endregion
    }

    public class CommentObyMapper
        : IMapper<ICommentOby>
    {
        public static CommentObyMapper New { get; } = new CommentObyMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            ICommentOby e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('R', 3, e1.OrderBy),
                ('L', 80, e1.Text)
            ).AddOLD(data);
        }

        public E Map<E>(
            ICommentOby e1) where E : ICommentOby, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                Text = e1.Text,
            };
        }
        #endregion
    }
}
