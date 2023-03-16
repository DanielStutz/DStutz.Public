using DStutz.Data.Efcos.Images;

// Version 1.1.0
namespace DStutz.Data.Pocos.Images
{
    public interface IImage
        : IPolyglot
    {
        public long Pk1 { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Source { get; set; }
        public string Copyright { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Tags { get; set; }
        public int? AzimuthAngle { get; set; }
        public int? PolarAngle { get; set; }
        public long? TextPk1 { get; set; }
        public long CategoryPk1 { get; set; }
    }

    public class ImageMPE
        : IPoco<IImage>, IImage
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Source { get; set; }
        public string Copyright { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Tags { get; set; }
        public int? AzimuthAngle { get; set; }
        public int? PolarAngle { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long? TextPk1 { get; set; }
        public ImageTextMPE? Text { get; set; }

        public long CategoryPk1 { get; set; }
        public ImageCategoryPE Category { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IImage, new()
        {
            return ImageMapper.New.Map<E>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(Text, ISOCode639);
        }
        #endregion
    }
}
