using DStutz.System.Joiners;

namespace DStutz.Data.Pocos.Addresses
{
    public class Addressee : IJoinableOld
    {
        #region Properties
        /***********************************************************/
        public string? Company { get; set; }
        public Person Person { get; set; }
        public Address Address { get; set; }
        #endregion

        #region Methods
        /***********************************************************/
        public bool IsCompany()
        {
            return Company != null
                && Company.Length > 0;
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
                    ('L', 30, Company),
                    ('L', 30, Person.GetGenderSurPreName()),
                    ('L', 60, Address.GetStreetAndCountry()),
                    ('L', 20, Address.Additional)
                );
            }
        }
        #endregion
    }
}
