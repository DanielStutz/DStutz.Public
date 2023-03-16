using DStutz.Data.Efcos.Images;

// Version 1.1.0
namespace DStutz.Data.Pocos.Images
{
    public class ImageCategoryPE
        : TreeNodePE<ImageCategoryPE, ImageCategoryDataMPO>
    { }

    public interface IImageCategoryData
        : IDeEnFr
    {
        public string? Abbr { get; set; }
        public string Href { get; set; }
        public string? Remark { get; set; }
    }

    public class ImageCategoryDataMPO
        : IPoco<IImageCategoryData>, IImageCategoryData, IPolyglot
    {
        #region Properties
        /***********************************************************/
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        public string? Abbr { get; set; }
        public string Href { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageCategoryDataMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IImageCategoryData, new()
        {
            return ImageCategoryDataMapper.New.Map<E>(this);
        }
        #endregion
    }
}
