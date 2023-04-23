using DStutz.Data.Efcos.Expert.Youtube;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert.Youtube
{
    public interface IPlaylist
    {
        public long Pk1 { get; set; }
        public string Title { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
        public long ChannelPk1 { get; set; }
    }

    public class PlaylistMPE
        : IPoco<IPlaylist>, IPlaylist
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Title { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long ChannelPk1 { get; set; }
        public ChannelMPE Channel { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return PlaylistMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IPlaylist, new()
        {
            return PlaylistMapper.New.Map<E>(this);
        }
        #endregion
    }
}
