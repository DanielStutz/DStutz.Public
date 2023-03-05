using DStutz.Data.Pocos.Addresses;
using DStutz.Data.Pocos.Companies;

namespace DStutz.View.Labels
{
    public class LabelAddress
        : Label
    {
        #region Properties
        /***********************************************************/
        private Address Address { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public LabelAddress(
            params string[] lines)
            : base(lines)
        { }

        public LabelAddress(
            Address address)
            : this(address, true)
        { }

        protected LabelAddress(
            Address address,
            bool addAddress = true)
        {
            if (address == null)
                throw new ArgumentNullException("Address");

            Address = address;

            if (addAddress)
                AddAddress();
        }

        protected LabelAddress(
            Addressee addressee)
        {
            if (addressee == null)
                throw new ArgumentNullException("Addressee");

            if (addressee.Address == null)
                throw new ArgumentNullException("Address");

            Address = addressee.Address;
        }

        protected LabelAddress(
            Company company)
        {
            if (company == null)
                throw new ArgumentNullException("Company");

            if (company.Person == null)
                throw new ArgumentNullException("Person");

            var address = company.GetAddress();

            if (address == null)
                throw new ArgumentNullException("Address");

            Address = address;
        }
        #endregion

        #region Methods - Company
        /***********************************************************/
        protected void AddCompany(
            string company,
            string attention,
            string title,
            Person person)
        {
            if (string.IsNullOrWhiteSpace(company))
                throw new ArgumentException("Company null or emtpy");

            // E.g. 'Stutz Toys' or 'Protechnic.ch GmbH'
            AppendLine(company);

            if (string.IsNullOrWhiteSpace(attention))
                throw new ArgumentException("Attention null or emtpy");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title null or emtpy");

            AddPerson($"{attention} {title}", person, true);
        }
        #endregion

        #region Methods - Person
        /***********************************************************/
        protected void AddPerson(
            string title,
            Person person,
            bool titleAndPersonIsOneLiner = false)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title null or emtpy");

            if (person == null)
                throw new ArgumentNullException("Person");

            if (titleAndPersonIsOneLiner)
            {
                AppendLine($"{title} {person.GetPreSurName()}");
            }
            else
            {
                AppendLine(title);
                AppendLine(person.GetPreSurName());
            }
        }
        #endregion

        #region Methods - Address
        /***********************************************************/
        protected void AddAddress()
        {
            if (!string.IsNullOrWhiteSpace(Address.Additional))
                AppendLine(Address.Additional);

            //if (!string.IsNullOrWhiteSpace(Address.AdditionalTwo))
            //    AppendLine(Address.AdditionalTwo);

            AppendLine(Address.GetStreet());

            if (Address.Place == null)
                throw new ArgumentNullException("Place");

            AppendLine(Address.Place.GetCodeName());
        }

        public void AddRegion()
        {
            if (Address.Region == null)
                throw new ArgumentNullException("Region");

            AppendLine(Address.Region.Name);
        }

        public void AddCountry()
        {
            if (Address.Country == null)
                throw new ArgumentNullException("Country");

            AppendLine(Address.Country.Name);
        }
        #endregion
    }
}
