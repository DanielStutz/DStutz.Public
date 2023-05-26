using DStutz.Data.Pocos.Images;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Images
{
    [Table("image_text")]
    public class ImageTextMEE
        : IEfco<ImageTextMPE>, IImageText
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageTextMapper.New.Joiner(this); }
        }

        public ImageTextMPE Map()
        {
            return ImageTextMapper.New.Map<ImageTextMPE>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion
    }

    public class ImageTextMapper
        : IMapper<IImageText>
    {
        public static ImageTextMapper New { get; } = new ImageTextMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IImageText e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR)
            ).Add(data);
        }

        public E Map<E>(
            IImageText e1) where E : IImageText, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
            };
        }
        #endregion
    }
}
