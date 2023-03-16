using DStutz.Data.Efcos.Logistics;

// Version 1.1.0
namespace DStutz.Data.Pocos.Logistics
{
    public interface IAddress
    {
        public long Pk1 { get; set; }
        public string StreetName { get; set; }
        public string? HouseNumber { get; set; }
        public string? Additional { get; set; }
        public long PlacePk1 { get; set; }
    }

    public class AddressMPE
        : IPoco<IAddress>, IAddress, IEquatable<AddressMPE>
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string StreetName { get; set; }
        public string? HouseNumber { get; set; }
        public string? Additional { get; set; }
        public long PlacePk1 { get; set; }
        #endregion

        #region Asymmetric code (methods implementing)
        /***********************************************************/
        public override bool Equals(object? other)
        {
            return other is AddressMPE address
                && Equals(address);
        }

        public bool Equals(AddressMPE? other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return Pk1 == other.Pk1 &&
                string.Equals(StreetName, other.StreetName) &&
                string.Equals(HouseNumber, other.HouseNumber) &&
                string.Equals(Additional, other.Additional);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pk1, StreetName, HouseNumber, Additional);
        }

        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return AddressMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IAddress, new()
        {
            return AddressMapper.New.Map<E>(this);
        }
        #endregion
    }
}
