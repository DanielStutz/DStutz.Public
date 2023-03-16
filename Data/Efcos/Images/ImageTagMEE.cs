using DStutz.Data.Pocos.Images;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Images
{
    [Table("image_tag")]
    public class ImageTagMEE
        : IEfco<ImageTagMPE>, IImageTag
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }

        [Column("tag")]
        public string Tag { get; set; }

        [Column("regex")]
        public string? Regex { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageTagMapper.New.Joiner(this); }
        }

        public ImageTagMPE Map()
        {
            return ImageTagMapper.New.Map<ImageTagMPE>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion
    }

    public class ImageTagMapper
        : IMapper<IImageTag>
    {
        public static ImageTagMapper New { get; } = new ImageTagMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IImageTag e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 10, e1.Type),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR),
                ('L', 20, e1.Tag),
                ('L', 20, e1.Regex)
            ).Add(data);
        }

        public E Map<E>(
            IImageTag e1) where E : IImageTag, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Type = e1.Type,
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
                Tag = e1.Tag,
                Regex = e1.Regex,
            };
        }
        #endregion
    }
}
