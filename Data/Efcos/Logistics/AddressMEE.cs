using DStutz.System.Joiners;

using DStutz.Data.Pocos.Logistics;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

// Version 1.1.0
namespace DStutz.Data.Efcos.Logistics
{
    [Table("address")]
    public class AddressMEE
        : IEfco<AddressMPE>, IAddress, IEquatableLambda<AddressMEE>
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("street_name")]
        public string StreetName { get; set; }

        [Column("house_number")]
        public string? HouseNumber { get; set; }

        [Column("additional")]
        public string? Additional { get; set; }

        [Column("place_pk1")]
        public long PlacePk1 { get; set; }
        #endregion

        #region Asymmetric code (methods implementing)
        /***********************************************************/
        public Expression<Func<AddressMEE, bool>> EqualsLambda()
        {
            return e =>
                e.PlacePk1 == PlacePk1 &&
                e.StreetName.Equals(StreetName) &&
                ((e.HouseNumber == null && HouseNumber == null) ||
                 (e.HouseNumber != null && e.HouseNumber.Equals(HouseNumber))) &&
                ((e.Additional == null && Additional == null) ||
                 (e.Additional != null && e.Additional.Equals(Additional)));
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AddressMapper.New.Joiner(this); }
        }

        public AddressMPE Map()
        {
            return AddressMapper.New.Map<AddressMPE>(this);
        }
        #endregion
    }

    public class AddressMapper
        : IMapper<IAddress>
    {
        public static AddressMapper New { get; } = new AddressMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IAddress e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 40, e1.StreetName),
                ('R', 10, e1.HouseNumber),
                ('L', 20, e1.Additional),
                ('L', 20, e1.PlacePk1)
            ).Add(data);
        }

        public E Map<E>(
            IAddress e1) where E : IAddress, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                StreetName = e1.StreetName,
                HouseNumber = e1.HouseNumber,
                Additional = e1.Additional,
                PlacePk1 = e1.PlacePk1,
            };
        }
        #endregion
    }
}
