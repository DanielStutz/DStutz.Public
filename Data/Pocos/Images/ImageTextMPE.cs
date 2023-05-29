using DStutz.Data.Efcos.Images;

// Version 1.1.0
namespace DStutz.Data.Pocos.Images
{
    public interface IImageText
        : IDeEnFrOLD, IPolyglotOLD
    {
        public long Pk1 { get; set; }
    }

    public class ImageTextMPE
        : IPoco<IImageText>, IImageText
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageTextMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IImageText, new()
        {
            return ImageTextMapper.New.Map<E>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion
    }
}
