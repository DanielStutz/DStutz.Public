using DStutz.System.Joiners;

using DStutz.Data.Efcos.Contacts;
using DStutz.Data.Pocos.Logistics;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Logistics
{
    [Table("shipment")]
    public class ShipmentMEE
        : IEfco<ShipmentMPE>, IShipment
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("address_pk1")]
        public long? AddressPk1 { get; set; }

        [ForeignKey("AddressPk1")]
        public AddressMEE? Address { get; set; }

        [Column("contact_pk1")]
        public long? ContactPk1 { get; set; }

        [ForeignKey("ContactPk1")]
        public ContactMEE? Contact { get; set; }

        [Column("tracking_pk1")]
        public long? TrackingPk1 { get; set; }

        [ForeignKey("TrackingPk1")]
        public TrackingMEE? Tracking { get; set; }
        #endregion

        #region Asymmetric code (database)
        /***********************************************************/
        public void Set(AddressMEE address)
        {
            AddressPk1 = address.Pk1;
            Address = address;
        }

        public void Set(ContactMEE contact)
        {
            ContactPk1 = contact.Pk1;
            Contact = contact;
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ShipmentMapper.New.Joiner(this); }
        }

        public ShipmentMPE Map()
        {
            return ShipmentMapper.New.Map<ShipmentMPE>(this);
        }
        #endregion
    }

    public class ShipmentMapper
        : IMapper<IShipment>
    {
        public static ShipmentMapper New { get; } = new ShipmentMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IShipment e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('R', 2, e1.OrderBy),
                ('L', 10, e1.Date.ToShortDateString()),
                ('L', 20, e1.Remark),
                ('L', 20, e1.AddressPk1),
                ('L', 20, e1.ContactPk1),
                ('L', 20, e1.TrackingPk1)
            ).Add(data);
        }

        public E Map<E>(
            IShipment e1) where E : IShipment, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                Date = e1.Date,
                Remark = e1.Remark,
                AddressPk1 = e1.AddressPk1,
                ContactPk1 = e1.ContactPk1,
                TrackingPk1 = e1.TrackingPk1,
            };

            if (typeof(E) == typeof(ShipmentMEE))
            {
                ShipmentMPE poco = (ShipmentMPE)(object)e1;
                ShipmentMEE efco = (ShipmentMEE)(object)e2;

                efco.Address =
                    Mapper.MapOptional(
                        poco.Address,
                        e => e.Map<AddressMEE>());

                efco.Contact =
                    Mapper.MapOptional(
                        poco.Contact,
                        e => e.Map<ContactMEE>());

                efco.Tracking =
                    Mapper.MapOptional(
                        poco.Tracking,
                        e => e.Map<TrackingMEE>());
            }
            else if (typeof(E) == typeof(ShipmentMPE))
            {
                ShipmentMEE efco = (ShipmentMEE)(object)e1;
                ShipmentMPE poco = (ShipmentMPE)(object)e2;

                poco.Address =
                    Mapper.MapOptional(
                        efco.Address,
                        e => e.Map());

                poco.Contact =
                    Mapper.MapOptional(
                        efco.Contact,
                        e => e.Map());

                poco.Tracking =
                    Mapper.MapOptional(
                        efco.Tracking,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}
