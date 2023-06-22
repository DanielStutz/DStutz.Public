using DStutz.System.Enums;

namespace DStutz.Data.Pocos.Logistics
{
    public sealed class Carrier
        : EnumAbbr<Carrier>
    {
        #region Properties
        /***********************************************************/
        public static readonly Carrier CHP = new Carrier("CHP", "Die Schweizerische Post AG", "www.post.ch/swisspost-tracking?formattedParcelCodes=#");
        public static readonly Carrier ATP = new Carrier("ATP", "Die Österreichische Post AG");
        public static readonly Carrier DEP = new Carrier("DEP", "Deutsche Post AG");

        public static readonly Carrier DHL = new Carrier("DHL");
        public static readonly Carrier DPD = new Carrier("DPD");
        public static readonly Carrier UPS = new Carrier("UPS");

        public string? TrackingLink { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public Carrier()
            : base()
        { }

        private Carrier(
            string abbr)
            : base(abbr, abbr)
        { }

        private Carrier(
            string abbr,
            string name)
            : base(abbr, name)
        { }

        private Carrier(
            string abbr,
            string name,
            string trackingLink)
            : base(abbr, name)
        {
            TrackingLink = trackingLink;
        }
        #endregion

        #region Methods converting
        /***********************************************************/
        public override Carrier Map(string? abbr)
        {
            if (abbr == null)
                throw new ArgumentNullException("Abbr");

            switch (abbr)
            {
                case "CHP":
                    return CHP;
                case "ATP":
                    return ATP;
                case "DEP":
                    return DEP;
                case "DHL":
                    return DHL;
                case "DPD":
                    return DPD;
                case "UPS":
                    return UPS;
                default:
                    throw EntityNotFoundException(this);
            }
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public string GetTrackingNumberShort(string trackingNumber)
        {
            if (trackingNumber == null)
                throw new ArgumentNullException("TrackingNumber");

            // TODO This should be added as a function per carrier
            return trackingNumber.Split('.').Last().TrimStart('0');
        }

        public string GetTrackingLink(string trackingNumber)
        {
            if (trackingNumber == null)
                throw new ArgumentNullException("TrackingNumber");

            if (TrackingLink == null)
                throw new ArgumentNullException("TrackingLink");

            return TrackingLink.Replace("#", trackingNumber);
        }
        #endregion
    }
}
