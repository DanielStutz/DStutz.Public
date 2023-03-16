using DStutz.Data.Efcos.Contacts;

// Version 1.1.0
namespace DStutz.Data.Pocos.Contacts
{
    public interface IContactDetailOby
        : IOrdered
    {
        public long Pk1 { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class ContactDetailObyMPE
        : IPoco<IContactDetailOby>, IContactDetailOby
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int OrderBy { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ContactDetailObyMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IContactDetailOby, new()
        {
            return ContactDetailObyMapper.New.Map<E>(this);
        }
        #endregion
    }
}
