using DStutz.Data.Pocos.Images;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Images
{
    [Table("image")]
    public class ImageMEE
        : IEfco<ImageMPE>, IImage
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("extension")]
        public string Extension { get; set; }

        [Column("source")]
        public string Source { get; set; }

        [Column("copyright")]
        public string Copyright { get; set; }

        [Column("width")]
        public int Width { get; set; }

        [Column("height")]
        public int Height { get; set; }

        [Column("tags")]
        public string? Tags { get; set; }

        [Column("azimuth")]
        public int? AzimuthAngle { get; set; }

        [Column("polar")]
        public int? PolarAngle { get; set; }
        #endregion

        //[Column("remark")]
        //public string? Remark { get; set; }

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("text_pk1")]
        public long? TextPk1 { get; set; }

        [ForeignKey("TextPk1")]
        public ImageTextMEE? Text { get; set; }

        [Column("category_pk1")]
        public long CategoryPk1 { get; set; }

        [ForeignKey("CategoryPk1")]
        public ImageCategoryMEE Category { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ImageMapper.New.Joiner(this); }
        }

        public ImageMPE Map()
        {
            return ImageMapper.New.Map<ImageMPE>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(Text, ISOCode639);
        }
        #endregion
    }

    public class ImageMapper
        : IMapper<IImage>
    {
        public static ImageMapper New { get; } = new ImageMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IImage e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 20, e1.Name),
                ('L', 3, e1.Extension),
                ('L', 3, e1.Source),
                ('L', 3, e1.Copyright),
                ('L', 4, e1.Width),
                ('L', 4, e1.Height),
                ('L', 20, e1.Tags),
                ('L', 20, e1.AzimuthAngle),
                ('L', 20, e1.PolarAngle),
                ('L', 20, e1.TextPk1),
                ('L', 20, e1.CategoryPk1)
            ).Add(data);
        }

        public E Map<E>(
            IImage e1) where E : IImage, new()
        {
            Mapper.CheckForWhiteSpaces(
                 $"Tags of image {e1.Pk1}",
                e1.Tags);

            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Name = e1.Name,
                Extension = e1.Extension,
                Source = e1.Source,
                Copyright = e1.Copyright,
                Width = e1.Width,
                Height = e1.Height,
                Tags = e1.Tags,
                AzimuthAngle = e1.AzimuthAngle,
                PolarAngle = e1.PolarAngle,
                TextPk1 = e1.TextPk1,
                CategoryPk1 = e1.CategoryPk1,
            };

            if (typeof(E) == typeof(ImageMEE))
            {
                ImageMPE poco = (ImageMPE)(object)e1;
                ImageMEE efco = (ImageMEE)(object)e2;

                efco.Text =
                    Mapper.MapOptional(
                        poco.Text,
                        e => e.Map<ImageTextMEE>());

                //efco.Category =
                //    Mapper.MapMandatory(
                //        poco.Category,
                //        e => e.Map<ImageCategoryEE>());
            }
            else if (typeof(E) == typeof(ImageMPE))
            {
                ImageMEE efco = (ImageMEE)(object)e1;
                ImageMPE poco = (ImageMPE)(object)e2;

                poco.Text =
                    Mapper.MapOptional(
                        efco.Text,
                        e => e.Map());

                poco.Category =
                    Mapper.MapMandatory(
                        efco.Category,
                        e => e.Map(false));
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}
