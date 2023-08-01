using DStutz.Data.Efcos.People;
using DStutz.Data.GEN.People;

// Version 1.1.0
namespace DStutz.Data.Pocos.People
{
    public interface IPerson
    {
        public long Pk1 { get; set; }
        public Gender Gender { get; set; }
        public string Prename { get; set; }
        public string Surname { get; set; }
    }

    public class PersonMPE
        : IPoco<IPerson>, IPerson, IEquatable<PersonMPE>
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public Gender Gender { get; set; }
        public string Prename { get; set; }
        public string Surname { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public PersonMPE()
        { }

        public PersonMPE(
            string gender)
        {
            Gender = GenderMapper.Map(gender);
        }

        public PersonMPE(
            Person person)
        {
            Gender = person.Gender;
            Prename = person.Prename;
            Surname = person.Surname;
        }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public override bool Equals(object? other)
        {
            return other is PersonMPE person
                && Equals(person);
        }

        public bool Equals(PersonMPE? other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return Pk1 == other.Pk1 &&
                Gender == other.Gender &&
                string.Equals(Prename, other.Prename) &&
                string.Equals(Surname, other.Surname);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Gender, Prename, Surname);
        }

        public string GetPreSurName()
        {
            return $"{Prename} {Surname}";
        }

        public string GetSurPreName()
        {
            return $"{Surname} {Prename}";
        }

        public string GetGenderSurPreName()
        {
            return $"{Gender.Abbr} {Surname} {Prename}";
        }

        public string GetPreSurNameShort()
        {
            return Prename[..1] + ". " + Surname;
        }

        public string GetSurPreNameShort()
        {
            return Surname + " " + Prename[..1] + ".";
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return PersonMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IPerson, new()
        {
            return PersonMapper.New.Map<E>(this);
        }
        #endregion
    }
}
