using DStutz.Data.Pocos.Addresses;

namespace DStutz.View.Labels
{
    public class LabelAddressContact
        : LabelAddress
    {
        #region Constructors
        /***********************************************************/
        public LabelAddressContact(
            string title,
            Addressee addressee,
            bool titleAndPersonIsOneLiner = false)
            : base(addressee)
        {
            AddPerson(
                title,
                addressee.Person,
                titleAndPersonIsOneLiner);

            AddAddress();
        }

        public LabelAddressContact(
            string title,
            Person person,
            Address address,
            bool titleAndPersonIsOneLiner = false)
            : base(address, false)
        {
            AddPerson(
                title,
                person,
                titleAndPersonIsOneLiner);

            AddAddress();
        }
        #endregion
    }
}
