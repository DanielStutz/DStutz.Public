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
    }
}
