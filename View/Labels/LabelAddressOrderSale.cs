using DStutz.Data.Pocos.Addresses;

namespace DStutz.View.Labels
{
    public class LabelAddressOrderSale
        : LabelAddress
    {
        #region Constructors
        /***********************************************************/
        public LabelAddressOrderSale(
            Addressee addressee,
            string attention,
            string title)
            : base(addressee)
        {
            if (addressee.IsCompany())
            {
                AddCompany(
                    addressee.Company!,
                    attention,
                    title,
                    addressee.Person);
            }
            else
            {
                AddPerson(
                    title,
                    addressee.Person,
                    false);
            }

            AddAddress();
        }
        #endregion
    }
}
