using DStutz.System.Joiners;

using DStutz.Data.Efcos.Logistics;

// Version 1.1
namespace DStutz.Data.Pocos.Logistics
{
    public interface ITracking
    {
        public long Pk1 { get; set; }
        public Carrier Carrier { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
    }

    public class TrackingMPE
        : IPoco<ITracking>, ITracking
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public Carrier Carrier { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string GetNumberShort()
        {
            return Carrier.GetTrackingNumberShort(Number);
        }

        public string GetLink()
        {
            return Carrier.GetTrackingLink(Number);
        }

        public string GetDate()
        {
            if (Date == null)
                return "";

            return ((DateTime)Date).ToShortDateString();
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return TrackingMapper.New.Joiner(this);
        }

        public E Map<E>() where E : ITracking, new()
        {
            return TrackingMapper.New.Map<E>(this);
        }
        #endregion
    }
}
