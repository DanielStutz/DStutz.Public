using DStutz.Data.Efcos.Youtube;

// Version 1.1.0
namespace DStutz.Data.Pocos.Youtube
{
    public interface IChannel
    {
        public long Pk1 { get; set; }
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string? Person { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
    }

    public class ChannelMPE
        : IPoco<IChannel>, IChannel
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string? Person { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
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
