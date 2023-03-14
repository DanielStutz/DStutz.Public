using DStutz.System.Joiners;

using DStutz.Data.Pocos.People;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.People
{
    [Table("feedback")]
    public class FeedbackMEE
        : IEfco<FeedbackMPE>, IFeedback
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("type")]
        public FeedbackType Type { get; set; }

        [Column("text")]
        public string Text { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return FeedbackMapper.New.Joiner(this); }
        }

        public FeedbackMPE Map()
        {
            return FeedbackMapper.New.Map<FeedbackMPE>(this);
        }
        #endregion
    }

    public class FeedbackMapper
        : IMapper<IFeedback>
    {
        public static FeedbackMapper New { get; } = new FeedbackMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IFeedback e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 1, e1.Type),
                ('L', 40, e1.Text)
            ).Add(data);
        }

        public E Map<E>(
            IFeedback e1) where E : IFeedback, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Type = e1.Type,
                Text = e1.Text,
            };
        }
        #endregion
    }
}
