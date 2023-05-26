using DStutz.System.Extensions;
using DStutz.System.Joiners;

namespace DStutz.Data.Pocos.Addresses
{
    public class Address : IJoinableOld
    {
        #region Properties
        /***********************************************************/
        public string StreetName { get; set; }
        public string? HouseNumber { get; set; }
        public string? Additional { get; set; }
        public Place Place { get; set; }
        public Region Region { get; set; }
        public Country Country { get; set; }
        #endregion

        #region Methods
        /***********************************************************/
        public string GetStreet()
        {
            return StreetName.Append(" ", HouseNumber);
        }

        public string GetStreetAndPlace(
            string separator = ", ")
        {
            return GetStreet().Append(separator, Place);
        }

        public string GetStreetAndRegion(
            string separator = ", ")
        {
            return GetStreet().Append(separator, Place, Region);
        }

        public string GetStreetAndCountry(
            string separator = ", ")
        {
            return GetStreet().Append(separator, Place, Region, Country);
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    //('L', 20, e1.GetType().Name),
                    ('L', 60, GetStreetAndCountry()),
                    ('L', 20, Additional)
                );
            }
        }
        #endregion
    }
}
