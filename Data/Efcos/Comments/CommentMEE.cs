using DStutz.Data.Pocos.Comments;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Comments
{
    public abstract class CommentMEE
        : IEfco<CommentMPE>, IComment
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("text")]
        public string Text { get; set; }
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
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 80, e1.Text)
            ).AddOLD(data);
        }

        public E Map<E>(
            IComment e1) where E : IComment, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Text = e1.Text,
            };
        }
        #endregion
    }
}
