using DStutz.Data.Pocos.Expert.Notes;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Notes
{
    [Table("note")]
    public class NoteMEE
        : IEfco<NoteMPE>, INote
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("lang")]
        public string Lang { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("text")]
        public string Text { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("author_pk1")]
        public long AuthorPk1 { get; set; }

        [ForeignKey("AuthorPk1")]
        public AuthorMEE Author { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return NoteMapper.New.Joiner(this); }
        }

        public NoteMPE Map()
        {
            return NoteMapper.New.Map<NoteMPE>(this);
        }
        #endregion
    }

    public class NoteMapper
        : IMapper<INote>
    {
        public static NoteMapper New { get; } = new NoteMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            INote e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 2, e1.Lang),
                ('L', 10, e1.Date),
                ('L', 60, e1.Title),
                ('L', 200, e1.Text),
                ('L', 20, e1.AuthorPk1)
            ).Add(data);
        }

        public E Map<E>(
            INote e1) where E : INote, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Lang = e1.Lang,
                Date = e1.Date,
                Title = e1.Title,
                Text = e1.Text,
                AuthorPk1 = e1.AuthorPk1,
            };

            if (typeof(E) == typeof(NoteMEE))
            {
                NoteMPE poco = (NoteMPE)(object)e1;
                NoteMEE efco = (NoteMEE)(object)e2;

                efco.Author =
                    Mapper.MapMandatory(
                        poco.Author,
                        e => e.Map<AuthorMEE>());
            }
            else if (typeof(E) == typeof(NoteMPE))
            {
                NoteMEE efco = (NoteMEE)(object)e1;
                NoteMPE poco = (NoteMPE)(object)e2;

                poco.Author =
                    Mapper.MapMandatory(
                        efco.Author,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}
