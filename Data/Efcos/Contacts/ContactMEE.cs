using DStutz.Data.Pocos.Contacts;
using DStutz.Data.Pocos.People;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

// Version 1.1.0
namespace DStutz.Data.Efcos.Contacts
{
    [Table("contact")]
    public class ContactMEE
        : IEfco<ContactMPE>, IContact, IEquatableLambda<ContactMEE>
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("company")]
        public string? Company { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("prename")]
        public string Prename { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }
        #endregion

        #region Relations 1:n (with default foreign key)
        /***********************************************************/
        [ForeignKey("Pk1")]
        public IReadOnlyList<ContactEmail>? Emails { get; set; }

        [ForeignKey("Pk1")]
        public IReadOnlyList<ContactPhone>? Phones { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public Expression<Func<ContactMEE, bool>> EqualsLambda()
        {
            return e =>
                e.Gender.Equals(Gender) &&
                e.Surname.Equals(Surname) &&
                e.Prename.Equals(Prename) &&
                ((e.Email == null && Email == null) ||
                 (e.Email != null && e.Email.Equals(Email))) &&
                ((e.Phone == null && Phone == null) ||
                 (e.Phone != null && e.Phone.Equals(Phone))) &&
                ((e.Company == null && Company == null) ||
                 (e.Company != null && e.Company.Equals(Company)));
        }

        public bool EqualsName(ContactMEE other)
        {
            return Surname.Equals(other.Surname) &&
                Prename.Equals(other.Prename);
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
                return new Joiner(
                    ('L', 20, Pk1),
                    ('L', 1, Gender),
                    ('L', 20, Surname),
                    ('L', 20, Prename),
                    ('L', 40, Email),
                    ('L', 40, Phone)
                );
        }

    public ContactMPE Map()
        {
            return ContactMapper.New.Map<ContactMPE>(this);
        }
        #endregion
    }

    [Table("contact_email")]
    public class ContactEmail : ContactDetailObyMEE
    { }

    [Table("contact_phone")]
    public class ContactPhone : ContactDetailObyMEE
    { }

    public class ContactMapper
        : IMapper<IContact>
    {
        public static ContactMapper New { get; } = new ContactMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IContact e1,
            params IJoinableOld?[] data)
        {
            throw new NotImplementedException();
        }

        public E Map<E>(
            IContact e1) where E : IContact, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Company = e1.Company,
            };

            if (typeof(E) == typeof(ContactMEE))
            {
                ContactMPE poco = (ContactMPE)(object)e1;
                ContactMEE efco = (ContactMEE)(object)e2;

                if (poco.Person != null)
                {
                    efco.Gender = poco.Person.Gender.Abbr;
                    efco.Surname = poco.Person.Surname;
                    efco.Prename = poco.Person.Prename;
                }

                efco.Emails =
                    Mapper.MapOptionalsOrdered(
                        poco.Emails,
                        e => e.Map<ContactEmail>());

                efco.Phones =
                    Mapper.MapOptionalsOrdered(
                        poco.Phones,
                        e => e.Map<ContactPhone>());

                // John.Doe@Dummy.ch --> john.doe@dummy.ch
                if (efco.Emails != null)
                {
                    foreach (var email in efco.Emails)
                        email.Value = email.Value.ToLower();

                    efco.Email = efco.Emails[0].Value;
                }

                // +41 79 218 76 20 --> +41792187620
                if (efco.Phones != null)
                {
                    foreach (var phone in efco.Phones)
                        phone.Value = phone.Value.Replace(" ", "");

                    efco.Phone = efco.Phones[0].Value;
                }
            }
            else if (typeof(E) == typeof(ContactMPE))
            {
                ContactMEE efco = (ContactMEE)(object)e1;
                ContactMPE poco = (ContactMPE)(object)e2;

                poco.Person = new PersonMPE(efco.Gender)
                {
                    Surname = efco.Surname,
                    Prename = efco.Prename,
                };

                poco.Emails =
                    Mapper.MapOptionals(
                        efco.Emails,
                        e => e.Map());

                poco.Phones =
                    Mapper.MapOptionals(
                        efco.Phones,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion

        #region Methods
        /***********************************************************/
        public string GetValues<T>(
            IEnumerable<T>? list)
            where T : IContactDetailOby
        {
            var values = "";

            if (list != null)
                values = string.Join(", ", list.Select(e => e.Value));

            return values;
        }
        #endregion
    }
}
