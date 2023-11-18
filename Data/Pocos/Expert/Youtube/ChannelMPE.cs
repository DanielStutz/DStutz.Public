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
    }
}
