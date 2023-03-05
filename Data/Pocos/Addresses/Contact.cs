using DStutz.System.Joiners;

namespace DStutz.Data.Pocos.Addresses
{
    public class Contact : IJoinable
    {
        #region Properties
        /***********************************************************/
        public string? Company { get; set; }
        public Person? Person { get; set; }
        public Address? Address { get; set; }
        public IReadOnlyList<ContactDetail>? Emails { get; set; }
        public IReadOnlyList<ContactDetail>? Phones { get; set; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public bool IsCompany()
        {
            return Company != null
                && Company.Length > 0;
        }

        public IJoiner Joiner()
        {
            var person = "";

            if (Person != null)
                person = Person.GetGenderSurPreName();

            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 30, Company),
                ('L', 30, person),
                ('L', 60, Address.GetStreetAndCountry()),
                ('L', 20, Address.Additional)
            );
        }
        #endregion
    }
}
