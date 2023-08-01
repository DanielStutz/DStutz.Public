using DStutz.Data.Pocos;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos
{
    [Table("weapon_wheel")]
    public class WeaponWheelMEE
        : IEfco<WeaponWheelMPE>, IWeaponWheel
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("primary_weapon")]
        public int PrimaryWeapon { get; set; }
        #endregion

        #region Properties owned
        /***********************************************************/
        public WeaponMEO SA { get; set; }  // Cols are 'sa_name' and 'sa_effect'
        public WeaponMEO PW1 { get; set; } // Cols are 'pw1_name' and 'pw1_effect'
        public WeaponMEO? PW2 { get; set; } // Cols are 'pw2_name' and 'pw2_effect'
        public WeaponMEO? PW3 { get; set; } // Cols are 'pw3_name' and 'pw3_effect'
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        //public IJoiner Joiner
        //{
        //    get { return WeaponWheelMapper.New.Joiner(this, SA, PW1, PW2, PW3); }
        //}

        public WeaponWheelMPE Map()
        {
            return WeaponWheelMapper.New.Map<WeaponWheelMPE>(this);
        }
        #endregion
    }

    public class WeaponWheelMapper
        : IMapper<IWeaponWheel>
    {
        public static WeaponWheelMapper New { get; } = new WeaponWheelMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IWeaponWheel e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 20, e1.PrimaryWeapon)
            ).Add(data);
        }

        public E Map<E>(
            IWeaponWheel e1) where E : IWeaponWheel, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                PrimaryWeapon = e1.PrimaryWeapon,
            };

            if (typeof(E) == typeof(WeaponWheelMEE))
            {
                WeaponWheelMPE poco = (WeaponWheelMPE)(object)e1;
                WeaponWheelMEE efco = (WeaponWheelMEE)(object)e2;

                efco.SA =
                    Mapper.MapMandatory(
                        poco.SA,
                        e => e.Map<WeaponMEO>());

                efco.PW1 =
                    Mapper.MapMandatory(
                        poco.PW1,
                        e => e.Map<WeaponMEO>());

                efco.PW2 =
                    Mapper.MapOptional(
                        poco.PW2,
                        e => e.Map<WeaponMEO>());

                efco.PW3 =
                    Mapper.MapOptional(
                        poco.PW3,
                        e => e.Map<WeaponMEO>());
            }
            else if (typeof(E) == typeof(WeaponWheelMPE))
            {
                WeaponWheelMEE efco = (WeaponWheelMEE)(object)e1;
                WeaponWheelMPE poco = (WeaponWheelMPE)(object)e2;

                poco.SA =
                    Mapper.MapMandatory(
                        efco.SA,
                        e => e.Map());

                poco.PW1 =
                    Mapper.MapMandatory(
                        efco.PW1,
                        e => e.Map());

                poco.PW2 =
                    Mapper.MapOptional(
                        efco.PW2,
                        e => e.Map());

                poco.PW3 =
                    Mapper.MapOptional(
                        efco.PW3,
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
