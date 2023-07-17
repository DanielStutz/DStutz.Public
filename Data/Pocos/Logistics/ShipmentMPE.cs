using DStutz.Apps.Services.Addresses;
using DStutz.Data.Efcos.Logistics;
using DStutz.Data.Pocos.Addresses;
using DStutz.Data.Pocos.Contacts;

using System.Text.Json.Serialization;

// Version 1.1.0
namespace DStutz.Data.Pocos.Logistics
{
    public interface IShipment
        : IDateTimed, IOrdered
    {
        public long Pk1 { get; set; }
        public string? Remark { get; set; }
        public long? AddressPk1 { get; set; }
        public long? ContactPk1 { get; set; }
        public long? TrackingPk1 { get; set; }
    }

    public class ShipmentMPE
        : IPoco<IShipment>, IShipment
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        [JsonPropertyName("Number")] public int OrderBy { get; set; }
        public DateTime Date { get; set; }
        public string? Remark { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [JsonIgnore] public long? AddressPk1 { get; set; }
        [JsonIgnore] public AddressMPE? Address { get; set; }
        [JsonIgnore] public long? ContactPk1 { get; set; }
        [JsonIgnore] public ContactMPE? Contact { get; set; }
        [JsonIgnore] public long? TrackingPk1 { get; set; }
        public TrackingMPE? Tracking { get; set; }
        #endregion

        #region Asymmetric code (json files)
        /***********************************************************/
        public Addressee? Addressee { get; set; }
        public void MapAddressee(
            IServicePlacesOLD service)
        {
            // Map json to poco
            if (Addressee != null &&
                Address == null &&
                Contact == null)
            {
                Address = service.Map(Addressee.Address);
                Contact = new ContactMPE(Addressee);
            }

            // Map poco to json
            if (Addressee == null &&
                Address != null &&
                Contact != null)
            {
                Addressee = new Addressee()
                {
                    Address = service.Map(Address),
                    Company = Contact.Company,
                };

                if (Contact.Person != null)
                {
                    Addressee.Person = new Person()
                    {
                        Gender = Contact.Person.Gender,
                        Prename = Contact.Person.Prename,
                        Surname = Contact.Person.Surname,
                    };
                }
            }

            Joiner.WriteRow();

            if (Tracking != null)
                Tracking.Joiner.WriteRow();

            if (Addressee != null)
                Addressee.Joiner.WriteRow();

            if (Address != null)
                Address.Joiner.WriteRow();

            if (Contact != null)
                Contact.Joiner.WriteRow();
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ShipmentMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IShipment, new()
        {
            return ShipmentMapper.New.Map<E>(this);
        }
        #endregion
    }
}
