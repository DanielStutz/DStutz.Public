using DStutz.Data.Efcos;

// Version 1.1.0
namespace DStutz.Data.Pocos
{
    public interface IWeapon
    {
        public string Name { get; set; }
        public double Effect { get; set; }
    }

    public class WeaponMPO
        : IPoco<IWeapon>, IWeapon
    {
        #region Properties
        /***********************************************************/
        public string Name { get; set; }
        public double Effect { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WeaponMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IWeapon, new()
        {
            return WeaponMapper.New.Map<E>(this);
        }
        #endregion
    }
}
