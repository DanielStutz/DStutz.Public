using DStutz.Data.Efcos.Contacts;
using DStutz.Data.Pocos.Addresses;
using DStutz.Data.Pocos.People;

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
        public long Pk1 { get; set; }
        public string? Company { get; set; }
        public PersonMPE Person { get; set; }

        /************************************************************
         * Constructors
         ************************************************************/
        public ContactMPE()
        { }

        public ContactMPE(
            Addressee addressee)
        {
            Company = addressee.Company;
            Person = new PersonMPE(addressee.Person);
        }

        /************************************************************
         * Relations 1:n (with default foreign key)
         ************************************************************/
        public IReadOnlyList<ContactDetailObyMPE>? Emails { get; set; }
        public IReadOnlyList<ContactDetailObyMPE>? Phones { get; set; }

        /************************************************************
         * Methods - Implementing
         ************************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    //(20, Pk1),
                    (80, Person.GetPreSurName()),
                    (40, GetEmails()),
                    (40, GetPhones())
                );
            }
        }

        public E Map<E>() where E : IContact, new()
        {
            return ContactMapper.New.Map<E>(this);
        }

        /************************************************************
         * Methods
         ************************************************************/
        public string GetEmails()
        {
            return ContactMapper.New.GetValues(Emails);
        }

        public string GetPhones()
        {
            return ContactMapper.New.GetValues(Phones);
        }
    }
}
