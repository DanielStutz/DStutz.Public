using DStutz.Data.Efcos;

// Version 1.1.0
namespace DStutz.Data.Pocos
{
    public interface IWeaponWheel
    {
        public long Pk1 { get; set; }
        public int PrimaryWeapon { get; set; }
    }

    public class WeaponWheelMPE
        : IPoco<IWeaponWheel>, IWeaponWheel
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int PrimaryWeapon { get; set; }
        #endregion

        #region Properties owned
        /***********************************************************/
        public WeaponMPO SA { get; set; } // Sidearm
        public WeaponMPO PW1 { get; set; } // Primary Weapon 1
        public WeaponMPO? PW2 { get; set; } // Primary Weapon 2
        public WeaponMPO? PW3 { get; set; } // Primary Weapon 3
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WeaponWheelMapper.New.Joiner(this, SA, PW1, PW2, PW3); }
        }

        public E Map<E>() where E : IWeaponWheel, new()
        {
            return WeaponWheelMapper.New.Map<E>(this);
        }
        #endregion
    }
}
