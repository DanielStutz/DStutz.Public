using DStutz.System.Joiners;

using DStutz.Data.Pocos.People;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.People
{
    [Table("person")]
    public class PersonMEE
        : IEfco<PersonMPE>, IPerson
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("prename")]
        public string Prename { get; set; }

        [Column("surname")]
        public string Surname { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return PersonMapper.New.Joiner(this); }
        }

        public PersonMPE Map()
        {
            return PersonMapper.New.Map<PersonMPE>(this);
        }
        #endregion
    }

    public class PersonMapper
        : IMapper<IPerson>
    {
        public static PersonMapper New { get; } = new PersonMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IPerson e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 1, e1.Gender),
                ('L', 40, e1.Prename),
                ('L', 40, e1.Surname)
            ).Add(data);
        }

        public E Map<E>(
            IPerson e1) where E : IPerson, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Gender = e1.Gender,
                Prename = e1.Prename,
                Surname = e1.Surname,
            };
        }
        #endregion
    }
}
