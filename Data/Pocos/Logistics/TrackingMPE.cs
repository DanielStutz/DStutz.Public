using DStutz.Data.Efcos.Logistics;
using DStutz.Data.GEN.Logistics;

// Version 1.1.0
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
        : ITracking
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
    }
}
