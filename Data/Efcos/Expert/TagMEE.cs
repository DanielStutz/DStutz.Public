using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("tag")]
    public class TagMEE
        : IEfco<TagMPE>, ITag
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
            get { return TagMapper.New.Joiner(this); }
        }

        public TagMPE Map()
        {
            return TagMapper.New.Map<TagMPE>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion
    }

    public class TagMapper
        : IMapper<ITag>
    {
        public static TagMapper New { get; } = new TagMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            ITag e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR)
            ).AddOLD(data);
        }

        public E Map<E>(
            ITag e1) where E : ITag, new()
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
