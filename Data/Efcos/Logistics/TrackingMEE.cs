using DStutz.Data.Pocos.Logistics;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Logistics
{
    [Table("tracking")]
    public class TrackingMEE
        : IEfco<TrackingMPE>, ITracking
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("carrier")]
        public Carrier Carrier { get; set; }

        [Column("number")]
        public string Number { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return TrackingMapper.New.Joiner(this); }
        }

        public TrackingMPE Map()
        {
            return TrackingMapper.New.Map<TrackingMPE>(this);
        }
        #endregion
    }

    public class TrackingMapper
        : IMapper<ITracking>
    {
        public static TrackingMapper New { get; } = new TrackingMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            ITracking e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 3, e1.Carrier.Abbr),
                ('L', 21, e1.Number),
                ('L', 10, e1.Date)
            ).Add(data);
        }

        public E Map<E>(
            ITracking e1) where E : ITracking, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Carrier = e1.Carrier,
                Number = e1.Number,
                Date = e1.Date,
            };
        }
        #endregion
    }
}
