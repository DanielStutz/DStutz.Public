using DStutz.Data.Efcos.Contacts;
using DStutz.Data.Pocos.Addresses;
using DStutz.Data.Pocos.People;

// Version 1.1.0
namespace DStutz.Data.Pocos.Contacts
{
    public interface IContact
    {
        public long Pk1 { get; set; }
        public string? Company { get; set; }
    }

    public class ContactMPE
        : IPoco<IContact>, IContact
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string? Company { get; set; }
        public PersonMPE Person { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        public IReadOnlyList<ContactDetailObyMPE>? Emails { get; set; }
        public IReadOnlyList<ContactDetailObyMPE>? Phones { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public ContactMPE()
        { }

        public ContactMPE(
            Addressee addressee)
        {
            Company = addressee.Company;
            Person = new PersonMPE(addressee.Person);
        }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string GetEmails()
        {
            return ContactMapper.New.GetValues(Emails);
        }

        public string GetPhones()
        {
            return ContactMapper.New.GetValues(Phones);
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    //(20, Pk1),
                    ('L', 80, Person.GetPreSurName()),
                    ('L', 40, GetEmails()),
                    ('L', 40, GetPhones())
                );
            }
        }

        public E Map<E>() where E : IContact, new()
        {
            return ContactMapper.New.Map<E>(this);
        }
        #endregion
    }
}
