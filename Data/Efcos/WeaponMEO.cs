using DStutz.Data.Pocos;

// Version 1.1.0
namespace DStutz.Data.Efcos
{
    public class WeaponMEO
        : IEfco<WeaponMPO>, IWeapon
    {
        #region Properties
        /***********************************************************/
        // See WeaponWheelMEE for column names
        public string Name { get; set; }

        // See WeaponWheelMEE for column names
        public double Effect { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return WeaponMapper.New.Joiner(this); }
        }

        public WeaponMPO Map()
        {
            return WeaponMapper.New.Map<WeaponMPO>(this);
        }
        #endregion
    }

    public class WeaponMapper
        : IMapper<IWeapon>
    {
        public static WeaponMapper New { get; } = new WeaponMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IWeapon e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 20, e1.Name),
                ('L', 4, e1.Effect)
            ).Add(data);
        }

        public E Map<E>(
            IWeapon e1) where E : IWeapon, new()
        {
            return new E()
            {
                Name = e1.Name,
                Effect = e1.Effect,
            };
        }
        #endregion
    }
}
