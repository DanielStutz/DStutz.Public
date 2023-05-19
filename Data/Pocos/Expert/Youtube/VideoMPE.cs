using DStutz.Data.Efcos.Expert.Youtube;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert.Youtube
{
    public interface IVideo
    {
        public long Pk1 { get; set; }
        public string Lang { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string? Href { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
        public long ChannelPk1 { get; set; }
        public long? PlaylistPk1 { get; set; }
    }

    public class VideoMPE
        : IPoco<IVideo>, IVideo
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Lang { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string? Href { get; set; }
        public string? Remark { get; set; }
        public string Identifier { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        public IReadOnlyList<CommentMPE>? Comments { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long ChannelPk1 { get; set; }
        public ChannelMPE Channel { get; set; }

        public long? PlaylistPk1 { get; set; }
        public PlaylistMPE? Playlist { get; set; }
        #endregion

        #region Relations m:n (with a junction table)
        /***********************************************************/
        public IReadOnlyList<RelPEAny<ProductMPE, IProduct>>? ProductRels { get; set; }
        public IReadOnlyList<RelPEAny<TagMPE, ITag>>? TagRels { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        [JsonIgnore]
        public IJoiner Joiner
        {
            get { return VideoMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IVideo, new()
        {
            return VideoMapper.New.Map<E>(this);
        }
        #endregion
    }
}
