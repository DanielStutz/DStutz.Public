using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("producer")]
    public class ProducerMEE
        : IEfco<ProducerMPE>, IProducer
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("abbr")]
        public string Abbr { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("country")]
        public string Country { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ProducerMapper.New.Joiner(this); }
        }

        public ProducerMPE Map()
        {
            return ProducerMapper.New.Map<ProducerMPE>(this);
        }
        #endregion
    }

    public class ProducerMapper
        : IMapper<IProducer>
    {
        public static ProducerMapper New { get; } = new ProducerMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IProducer e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 3, e1.Abbr),
                ('L', 40, e1.Name),
                ('L', 80, e1.Href),
                ('L', 2, e1.Country)
            ).AddOLD(data);
        }

        public E Map<E>(
            IProducer e1) where E : IProducer, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Abbr = e1.Abbr,
                Name = e1.Name,
                Href = e1.Href,
                Country = e1.Country,
            };
        }
        #endregion
    }
}
