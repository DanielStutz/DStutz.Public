using DStutz.Data.Pocos.Expert.Websites;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert.Websites
{
    [Table("website_series")]
    public class SeriesMEE
        : IEfco<SeriesMPE>, ISeries
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("author_pk1")]
        public long AuthorPk1 { get; set; }

        [ForeignKey("AuthorPk1")]
        public AuthorMEE? Author { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return SeriesMapper.New.Joiner(this); }
        }

        public SeriesMPE Map()
        {
            return SeriesMapper.New.Map<SeriesMPE>(this);
        }
        #endregion
    }

    public class SeriesMapper
        : IMapper<ISeries>
    {
        public static SeriesMapper New { get; } = new SeriesMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            ISeries e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 60, e1.Title),
                ('L', 80, e1.Href),
                ('L', 40, e1.Remark),
                ('L', 20, e1.AuthorPk1)
            ).AddOLD(data);
        }

        public E Map<E>(
            ISeries e1) where E : ISeries, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Title = e1.Title,
                Href = e1.Href,
                Remark = e1.Remark,
                AuthorPk1 = e1.AuthorPk1,
            };

            if (typeof(E) == typeof(SeriesMEE))
            {
                SeriesMPE poco = (SeriesMPE)(object)e1;
                SeriesMEE efco = (SeriesMEE)(object)e2;

                efco.Author =
                    Mapper.MapOptional(
                        poco.Author,
                        e => e.Map<AuthorMEE>());
            }
            else if (typeof(E) == typeof(SeriesMPE))
            {
                SeriesMEE efco = (SeriesMEE)(object)e1;
                SeriesMPE poco = (SeriesMPE)(object)e2;

                poco.Author =
                    Mapper.MapOptional(
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
