using DStutz.Data.Efcos.Images;

// Version 1.1.0
namespace DStutz.Data.Pocos.Images
{
    public interface IImageTag
        : IDeEnFr, IPolyglot
    {
        public long Pk1 { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public string? Regex { get; set; }
    }

    public class ImageTagMPE
        : IPoco<IImageTag>, IImageTag
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Type { get; set; }
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        public string Tag { get; set; }
        public string? Regex { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageTagMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IImageTag, new()
        {
            return ImageTagMapper.New.Map<E>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion
    }
}
