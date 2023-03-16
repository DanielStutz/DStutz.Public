using DStutz.Data.Pocos.Images;

using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Images
{
    [Table("image_category")]
    public class ImageCategoryMEE
        : TreeNodeMEE<ImageCategoryMEE, ImageCategoryPE, ImageCategoryDataMEO, ImageCategoryDataMPO>
    { }

    public class ImageCategoryDataMEO
        : IEfco<ImageCategoryDataMPO>, IImageCategoryData
    {
        #region Properties
        /***********************************************************/
        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }

        [Column("abbr")]
        public string? Abbr { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageCategoryDataMapper.New.Joiner(this); }
        }

        public ImageCategoryDataMPO Map()
        {
            return ImageCategoryDataMapper.New.Map<ImageCategoryDataMPO>(this);
        }
        #endregion
    }

    public class ImageCategoryDataMapper
        : IMapper<IImageCategoryData>
    {
        public static ImageCategoryDataMapper New { get; } = new ImageCategoryDataMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IImageCategoryData e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR),
                ('L', 3, e1.Abbr),
                ('L', 30, e1.Href),
                ('L', 20, e1.Remark)
            ).Add(data);
        }

        public E Map<E>(
            IImageCategoryData e1) where E : IImageCategoryData, new()
        {
            return new E()
            {
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
                Abbr = e1.Abbr,
                Href = e1.Href,
                Remark = e1.Remark,
            };
        }
        #endregion
    }
}
