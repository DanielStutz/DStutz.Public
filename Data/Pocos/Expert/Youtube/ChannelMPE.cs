using DStutz.Data.Efcos.Expert.Youtube;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert.Youtube
{
    public interface IChannel
    {
        public long Pk1 { get; set; }
        public string Name { get; set; }
        public string? Href { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
        public long AuthorPk1 { get; set; }
    }

    public class ChannelMPE
        : IPoco<IChannel>, IChannel
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Name { get; set; }
        public string? Href { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
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
            get { return ChannelMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IChannel, new()
        {
            return ChannelMapper.New.Map<E>(this);
        }
        #endregion
    }
}
