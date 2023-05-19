using DStutz.Data.Efcos.Expert.Websites;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert.Websites
{
    public interface ISeries
    {
        public long Pk1 { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string? Remark { get; set; }
        public long AuthorPk1 { get; set; }
    }

    public class SeriesMPE
        : IPoco<ISeries>, ISeries
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long AuthorPk1 { get; set; }
        public AuthorMPE Author { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        [JsonIgnore]
        public IJoiner Joiner
        {
            get { return SeriesMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : ISeries, new()
        {
            return SeriesMapper.New.Map<E>(this);
        }
        #endregion
    }
}
