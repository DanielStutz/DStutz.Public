using DStutz.Data.Pocos.Addresses;
using DStutz.Data.Pocos.Companies;

namespace DStutz.View.Labels
{
    public class LabelAddressCompany
        : LabelAddress
    {
        #region Constructors
        /***********************************************************/
        public LabelAddressCompany(
            Company company)
            : base(company)
        {
            AppendLine(company.TradeName);
            AppendLine(company.Person.GetPreSurName());
            AddAddress();
        }

        public LabelAddressCompany(
            Company company,
            string attention,
            string title)
            : base(company)
        {
            AddCompany(
                company.TradeName,
                attention,
                title,
                company.Person);

            AddAddress();
        }

        public LabelAddressCompany(
            string company,
            Person person,
            Address address)
            : base(address, false)
        {
            AppendLine(company);
            AppendLine(person.GetPreSurName());
            AddAddress();
        }

        public LabelAddressCompany(
            string company,
            string attention,
            string title,
            Person person,
            Address address)
            : base(address, false)
        {
            AddCompany(
                company,
                attention,
                title,
                person);

            AddAddress();
        }
        #endregion
    }
}
