using DStutz.Data.Pocos.Contacts;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Contacts
{
    public abstract class ContactDetailObyMEE
        : IEfco<ContactDetailObyMPE>, IContactDetailOby
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("value")]
        public string Value { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ContactDetailObyMapper.New.Joiner(this); }
        }

        public ContactDetailObyMPE Map()
        {
            return ContactDetailObyMapper.New.Map<ContactDetailObyMPE>(this);
        }
        #endregion
    }

    public class ContactDetailObyMapper
        : IMapper<IContactDetailOby>
    {
        public static ContactDetailObyMapper New { get; } = new ContactDetailObyMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IContactDetailOby e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('R', 3, e1.OrderBy),
                ('L', 1, e1.Type),
                ('L', 40, e1.Value)
            ).Add(data);
        }

        public E Map<E>(
            IContactDetailOby e1) where E : IContactDetailOby, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                Type = e1.Type,
                Value = e1.Value,
            };
        }
        #endregion
    }
}
