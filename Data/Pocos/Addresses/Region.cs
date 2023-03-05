using DStutz.System.Joiners;

namespace DStutz.Data.Pocos.Addresses
{
    public class Region : CodeName, IJoinable
    {
        #region Properties
        /***********************************************************/
        public string? CountryCode { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public Region()
        { }

        public Region(
            string code,
            string name)
        {
            Code = code;
            Name = name;
        }

        public Region(
            string code,
            string name,
            string countryCode)
        {
            Code = code;
            Name = name;
            CountryCode = countryCode;
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return new Joiner(
                (6, Code),
                (40, Name),
                (3, CountryCode)
            );
        }
        #endregion
    }
}
