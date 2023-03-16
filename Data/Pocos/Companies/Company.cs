using DStutz.Data.Pocos.Addresses;
using DStutz.Data.Pocos.Contacts;
using DStutz.System.Exceptions;

namespace DStutz.Data.Pocos.Companies
{
    public class Company : IJoinable
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string UniqueId { get; set; }
        public string LegalName { get; set; }
        public string TradeName { get; set; }
        public Person Person { get; set; }
        public List<AddressDated> Addresses { get; set; }
        public List<ContactDetailObyMPE> Emails { get; set; }
        public List<ContactDetailObyMPE> Phones { get; set; }
        #endregion

        #region Methods address
        /***********************************************************/
        public Address GetAddress()
        {
            return GetAddress(DateTime.Now);
        }

        public Address GetAddress(string date)
        {
            return GetAddress(DateTime.Parse(date));
        }

        public Address GetAddress(DateTime date)
        {
            foreach (var office in Addresses)
            {
                if (office.Date1 <= date &&
                    date <= office.Date2)
                {
                    return office.Address;
                }
            }

            throw new NotFoundException(
                "Address for date", date.ToShortDateString());
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    (3, UniqueId),
                    (30, LegalName),
                    (30, TradeName)
                );
            }
        }
        #endregion
    }
}
